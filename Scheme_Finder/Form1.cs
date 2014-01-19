using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Scheme_Finder
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // Internal variables
        private string  sourceFilePath, destinationFilePath;
        private float targetLength, targetScore, targetTolerance;

        private void Form1_Load(object sender, EventArgs e)
        {
            // !!! initialise list of sections

            // !!! initialise list of list of sections
        }

        // Display 'About' information
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Scheme Finder V1.1\nWritten by Dean Findlay\nDean.Findlay@Derbyshire.Gov.UK");
        }

        // Set source file path
        private void btn_Source_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            // If filter index set to 2 the the second half of the above argument would be used
            // allowing the usage of all file types.
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            // Checks whether the file dialog was succesfull and assigns
            // the path to openFilePath
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceFilePath = openFileDialog1.FileName;
                txt_DisplaySource.Text = sourceFilePath;
            }
        }

        // Set destination
        private void btn_Destination_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            // checks whether the file dialog was succesfull and assigns
            // the path to saveFilePath
            DialogResult saveResult = folderBrowserDialog1.ShowDialog();
            if (saveResult == DialogResult.OK)
            {
                destinationFilePath = folderBrowserDialog1.SelectedPath;
                txt_DisplayDestination.Text = destinationFilePath;
            }            
        }

        // !!! Start program running
        private void btn_Start_Click(object sender, EventArgs e)
        {
            // Checks to ensure a valid source/destinaton path have been given
            if (sourceFilePath == null)
            {
                MessageBox.Show("No source path specified");
                return;
            }
            if (destinationFilePath == null)
            {
                MessageBox.Show("No destination path specified");
                return;
            }

            // Checks that valid numeric variables have been entered
            if ((Helpers.validNumber(txt_Length.Text, ref targetLength, "length")) &&
                (Helpers.validNumber(txt_Score.Text, ref targetScore, "score")) &&
                (Helpers.validNumber(txt_Tolerance.Text, ref targetTolerance, "tolerance")))
            {
                this.Text = "Scheme finder - Extracting";
                // Initate instance of FinndSchemes and start program
                FindSchemes run = new FindSchemes();
                run.StartFinder(sourceFilePath, destinationFilePath
                    , targetLength, targetScore, targetTolerance);

                this.Text = "Scheme Finder";
            }
        }

        // !!! Show selected scheme from lvw_SchemeSummary in lvw_Scheme
        private void btn_ShowScheme_Click(object sender, EventArgs e)
        {

        }   
     
        // !!! Once extraction is complete set all path and target variables to null
        // so that if new variables are attempted that are not valid the program does
        // not just use the old variables
    }

    // Collection of static helpers used throughout the program
    public static class Helpers
    {        

        // Checks for valid numbers
        public static bool validNumber(string text, ref float num, string variable)
        {
            bool isNum = float.TryParse(text, out num);

            // Checks to see if a number is given
            if (isNum)
            {
                // Checks if the given number is greater than 0
                if (num <= 0)
                {
                    MessageBox.Show("Please enter a " + variable + " > 0");
                    return false;
                }
                return true;
            }
            else
            {
                MessageBox.Show("Please enter a valid " + variable);
                return false;
            }
        }
    }

    // !!! Controls the flow of the program
    public class FindSchemes
    {
        // variables from form
        private string sourcePath, destinationPath;
        private float targetLength, targetScore, targetTolerance;

        // !!! variables to hold relevant information on the current scheme being processed
        // LaneA denotes the first lane that triggered the call while LaneB denotes the opposite side.
        private float laneAStartChain, laneAEndChain;
        private float laneBStartChain, laneBEndChain;

        // !!! Starting point for program
        public void StartFinder(string formSource, string formDestination
            , float formLength, float formScore, float formTol)
        {
            // Assign variables passed from form
            this.sourcePath = formSource;
            this.destinationPath = formDestination;
            this.targetLength = formLength;
            this.targetScore = formScore;
            this.targetTolerance = formTol;

            // readers and writers
            FileInfo sourceFile = new FileInfo(sourcePath);
            StreamReader reader = sourceFile.OpenText();
            FileInfo Viable_SchemeFile = new FileInfo(destinationPath + "\\Viable_Scheme.txt");
            StreamWriter Viable_SchemeWriter = Viable_SchemeFile.CreateText();
            FileInfo Opposite_SideFile = new FileInfo(destinationPath + "\\Opposite_Side.txt");
            StreamWriter Opposite_SideWriter = Opposite_SideFile.CreateText();
            FileInfo Both_SidesFile = new FileInfo(destinationPath + "\\Both_Sides.txt");
            StreamWriter Both_SidesWriter = Both_SidesFile.CreateText();

            // Queue of Sections
            Queue<Section> queueSection = new Queue<Section>();

            // Create a text variable to hold each line
            string readText;

            // Assign the first line of the file to 'text'
            readText = reader.ReadLine();

            // values that will be assigned at end of each readText loop
            // for comparrison checks against the newly read text
            string previousRoad_No = "start", previousSection_No = "Start", previousLane = "Start";

            // values with data on the current queue state
            float currentQueueLength = 0;
            float currentAggregateQueueScore = 0;
            
            // !!! walk the file and read every line calling to write methods where applicable
            while (readText != null)
            {
                // split text into fields and return as a Section
                Section tempSection = splitText(readText);


                // Checks to see if road_No or Lane have changed
                // if yes then clear queue and add section
                // if no then add section to queue
                if ((tempSection.road_No != previousRoad_No) ||
                    (tempSection.lane != previousLane))
                {
                    queueSection.Clear();
                    queueSection.Enqueue(tempSection);
                }
                else
                {
                    queueSection.Enqueue(tempSection);
                }
         

                // gets the current length of all the sections in the queue
                currentQueueLength = QueueSection.sumQueueSectionLength(queueSection);

                // while queue length over target length; check and do relevant call
                // at end of loop dequeue to temp section and subtract it's length from queue length
                // if length still over then run process again
                // this will ensure that a spate of large section lengths will not produce larger schemes
                while (currentQueueLength > targetLength)
                {

                    // aggregate score = total sum of scores / total length of queue
                    currentAggregateQueueScore = 
                        QueueSection.sumQueueSectionScore(queueSection, currentQueueLength);

                    // checks if aggregate score greater than target score
                    if (currentAggregateQueueScore > targetScore)
                    {
                        laneAEndChain = tempSection.end_Chain;
                        // SchemeStCh set to an unrealistic No in order to be overwritten later
                        laneAStartChain = 999999;
                        // call to write viable_scheme
                        Viable_SchemeWriter.WriteLine("Scheme with score of " + currentAggregateQueueScore);
                        // prints queue while also retaining the first start chain in the queue
                        QueueSection.PrintQueue(queueSection, Viable_SchemeWriter, ref laneAStartChain);
                        Viable_SchemeWriter.WriteLine();

                        
                        // creates queue and populates it with the opposite lane scheme
                        Queue<Section> OppositeSideScheme = new Queue<Section>();
                        // !!! call to write opposite_Side
                        // check if section_No changes and call as appropriate
                        Section startSection = queueSection.Peek();
                        if (tempSection.section_No == startSection.section_No)
                        {
                            BothSchemes(tempSection.road_No, tempSection.section_No, tempSection.lane
                                , laneAStartChain, laneAEndChain, OppositeSideScheme);
                        }
                        else
                        {
                            BothSchemes(tempSection.road_No, startSection.section_No, tempSection.section_No
                                ,tempSection.lane, laneAStartChain, laneAEndChain, OppositeSideScheme);
                        }

                        // finds length of oppositeSideScheme
                        float oppSideLength = QueueSection.sumQueueSectionLength(OppositeSideScheme);

                        // sums the scores in the queue
                        float oppScoreSum = QueueSection.sumQueueSectionScore(OppositeSideScheme
                            , oppSideLength);

                        // Writes the origional queue to file
                        Opposite_SideWriter.WriteLine("Scheme with score of " + currentAggregateQueueScore);
                        QueueSection.PrintQueue(queueSection, Opposite_SideWriter);
                        Opposite_SideWriter.WriteLine();
                        // Writes the origional queue to file
                        Opposite_SideWriter.WriteLine("Opposite side scheme with score of " + oppScoreSum);
                        QueueSection.PrintQueue(OppositeSideScheme, Opposite_SideWriter);
                        Opposite_SideWriter.WriteLine();                            
                            
                        // !!! call to write both_Sides
                        if (oppScoreSum >= targetScore)
                        {
                            // Writes the origional queue to file
                            Both_SidesWriter.WriteLine("Scheme with score of " + currentAggregateQueueScore);
                            QueueSection.PrintQueue(queueSection, Both_SidesWriter);
                            Both_SidesWriter.WriteLine();
                            // Writes the origional queue to file
                            Both_SidesWriter.WriteLine("Opposite side scheme with score of " + oppScoreSum);
                            QueueSection.PrintQueue(OppositeSideScheme, Both_SidesWriter);
                            Both_SidesWriter.WriteLine();
                        }
                    }


                    // Dequeue 1 section and subtract it's length from currentQueueLength to avoid recalculating
                    Section tempSec = queueSection.Dequeue();
                    currentQueueLength -= tempSec.length;
                }

                // assign current variables to previous variables for comparrison on next loop
                previousRoad_No = tempSection.road_No;
                previousSection_No = tempSection.section_No;
                previousLane = tempSection.lane;

                // Assign next line of sourcefile to text
                readText = reader.ReadLine();

                // !!! temp write for testing
                //Viable_SchemeWriter.WriteLine(queueSection.Count.ToString() + ", " + currentQueueLength + ", " + currentAggregateQueueScore);
                
            }

            MessageBox.Show("Extraction complete");

        }

        // returns section from given text
        private Section splitText(string text)
        {
            // variables to hold individual fields from readText
            int tempId = 0;
            string tempRoad_No = "empty", tempSection_No = "empty", tempLane = "empty";
            float tempStart_Chain = 0, tempEndChain = 0;
            float tempLength = 0, tempScore = 0;

            char[] delimiters = new char[] { '\t' };
            string[] parts = text.Split(delimiters);
            for (int i = 0; i < parts.Length; i++)
            {
                // !!! change all code to 'tryParse' and throw error if incorrect
                switch (i)
                {
                    case 0:
                        tempId = int.Parse(parts[i]);
                        break;
                    case 1:
                        tempRoad_No = parts[i];
                        break;
                    case 2:
                        tempSection_No = parts[i];
                        break;
                    case 3:
                        tempLane = parts[i];
                        break;
                    case 4:
                        tempStart_Chain = float.Parse(parts[i]);
                        break;
                    case 5:
                        tempEndChain = float.Parse(parts[i]);
                        break;
                    case 6:
                        tempLength = float.Parse(parts[i]);
                        break;
                    case 7:
                        tempScore = float.Parse(parts[i]);
                        break;
                    default:
                        MessageBox.Show("Error occured while reading input file");
                        break;
                }
            }

            Section tempSec = new Section(tempId, tempRoad_No, tempSection_No, tempLane
                , tempStart_Chain ,tempEndChain, tempLength, tempScore);
            return tempSec;
        }

        // Uses info from a viable scheme found to find the matching sections on the opposite lane
        // when the section_No stays the same throughout the scheme
        private void BothSchemes(string schemeRoad, string schemeSctn, string schemeLane
            ,float schemeStart, float schemeEnd, Queue<Section> OppSections)
        {
            // Open a file for use
            FileInfo theSourceFile = new FileInfo(sourcePath);

            // Create a text reader for above file
            StreamReader laneReader = theSourceFile.OpenText();

            bool queuing = false;
            bool exit = false;
            float oppEndChain = schemeEnd + targetTolerance;
            float oppStartChain = laneAStartChain - targetTolerance;
            if (oppStartChain < 0.0f)
            {
                oppStartChain = 0.0f;
            }

            // Iterate through B_C.txt queing any sections that fall whithin the start and end chains of the 
            //relevant roadNo sectionNo and opposite lane

            // Assign the first line of the file to 'text'
            string laneText = laneReader.ReadLine();

            // Loops while not EOF or told to exit
            while ((laneText != null) && (exit == false))
            {
                // split text into fields and return as a Section
                Section laneSection = splitText(laneText);

                // Start queing once the correct sections are read in
                if ((laneSection.road_No == schemeRoad) &&
                    (laneSection.section_No == schemeSctn) &&
                    (laneSection.lane != schemeLane) &&
                    (laneSection.start_Chain >= oppStartChain))
                {
                    queuing = true;
                }

                if (queuing == true)
                {
                    // Stop queing if road no, section no or lane changes as well as if
                    // the end chain criteria is met
                    if ((laneSection.road_No != schemeRoad) ||
                        (laneSection.section_No != schemeSctn) ||
                        (laneSection.lane == schemeLane) ||
                        (laneSection.end_Chain >= oppEndChain))
                    {
                        queuing = false;
                        // Exit the method so the rest of the txt file is not read
                        exit = true;
                    }
                }

                if (queuing == true)
                {
                    OppSections.Enqueue(laneSection);
                }

                // Read in next line of text
                laneText = laneReader.ReadLine();
            }
        }

        // Uses info from a viable scheme found to find the matching sections on the opposite lane
        // when the section_No changes during the scheme
        private void BothSchemes(string schemeRoad, string schemeStartSctn, string schemeEndSctn
            ,string schemeLane, float schemeStart, float schemeEnd, Queue<Section> OppSections)
        {
            // Open a file for use
            FileInfo theSourceFile = new FileInfo(sourcePath);

            // Create a text reader for above file
            StreamReader laneReader = theSourceFile.OpenText();

            bool queuing = false;
            bool exit = false;
            bool sectionChanged = false;
            float oppEndChain = schemeEnd + targetTolerance;
            float oppStartChain = laneAStartChain - targetTolerance;
            if (oppStartChain < 0.0f)
            {
                oppStartChain = 0.0f;
            }

            // Iterate through B_C.txt queing any sections that fall whithin the start and end chains of the 
            //relevant roadNo sectionNo and opposite lane

            // Assign the first line of the file to 'text'
            string laneText = laneReader.ReadLine();

            // Loops while not EOF or told to exit
            while ((laneText != null) && (exit == false))
            {
                // split text into fields and return as a Section
                Section laneSection = splitText(laneText);

                // Start queing once the correct sections are read in
                if ((laneSection.road_No == schemeRoad) &&
                    (laneSection.section_No == schemeStartSctn) &&
                    (laneSection.lane != schemeLane) &&
                    (laneSection.start_Chain >= oppStartChain))
                {
                    queuing = true;
                }

                if (queuing == true)
                {

                    // Stop queuing if road_No or Lane changes (this shouldn't happen)
                    if ((laneSection.road_No != schemeRoad) ||
                        (laneSection.lane == schemeLane))
                    {
                        queuing = false;
                        // Exit the method so the rest of the txt file is not read
                        exit = true;
                    }


                    // Check for when the section_No changes to new section and change bool
                    if (laneSection.section_No == schemeEndSctn)
                    {
                        sectionChanged = true;
                    }


                    if (sectionChanged == true)
                    {
                        // once laneChanged == true check for end_chain condition or section_No change
                        if ((laneSection.section_No != schemeEndSctn) ||
                            (laneSection.end_Chain >= oppEndChain))
                        {
                            queuing = false;
                            // Exit the method so the rest of the txt file is not read
                            exit = true;
                        }
                    }
                    
                }

                if (queuing == true)
                {
                    OppSections.Enqueue(laneSection);
                }

                // Read in next line of text
                laneText = laneReader.ReadLine();
            } 
        }
    }

    // !!! Struct to hold Section + related tools
    public struct Section
    {
        public int id;
        public string road_No, section_No, lane;
        public float start_Chain, end_Chain, length, score;


        // Constructor assigns section from given parameters
        public Section(int id, string road, string section, string lane
            , float start, float end, float length, float score)
        {
            this.id = id;
            this.road_No = road;
            this.section_No = section;
            this.lane = lane;
            this.start_Chain = start;
            this.end_Chain = end;
            this.length = length;
            this.score = score;
        }

        // prints the fields of a section to a given textWriter
        public void PrintSection(StreamWriter writer)
        {
            writer.Write(this.id + "," + this.road_No + "," + this.section_No + "," + this.lane + ","
                    + this.start_Chain + "," + this.end_Chain + "," + this.length + ", " + this.score);
            writer.WriteLine();
        }
    }
    
    // tools for working with Queue<Section>'s
    public static class QueueSection
    {
        // returns total length of queue
        public static float sumQueueSectionLength(Queue<Section> tempQueueSection)
        {
            float total = 0.0f;

            foreach (Section sect in tempQueueSection)
            {
                total += sect.length;
            }

            return total;
        }


        // returns aggregate score of queue
        public static float sumQueueSectionScore(Queue<Section> tempQueueSection, float queueLength)
        {
            float total = 0.0f;

            foreach (Section sect in tempQueueSection)
            {
                total += sect.score / queueLength * sect.length;
            }

            return total;
        }

        // Used to print the queue while also assigning the startChain variable with the first start chain in the queue
        public static void PrintQueue(Queue<Section> Sections, StreamWriter writer, ref float startChain)
        {
            Section tempSection = new Section();
            System.Collections.IEnumerator en = Sections.GetEnumerator();

            writer.AutoFlush = true;

            while (en.MoveNext())
            {
                tempSection = (Section)en.Current;
                if (startChain == 999999)
                {
                    startChain = tempSection.start_Chain;
                }

                // print section using supplied writer
                tempSection.PrintSection(writer);
            }
        }

        // Prints the sections in the queue
        public static void PrintQueue(Queue<Section> Sections, StreamWriter writer)
        {
            Section tempSection = new Section();
            System.Collections.IEnumerator en = Sections.GetEnumerator();

            writer.AutoFlush = true;

            while (en.MoveNext())
            {
                tempSection = (Section)en.Current;
                // print section using supplied writer
                tempSection.PrintSection(writer);
            }
        }
    }


    // !!! tools for working with list<Section>'s
    public static class ListSection
    {
        // !!! find score of scheme
    }

    // !!! tools for working with list<list<Section>>'s
    public static class ListListSection // may have to add interface here such as iSortable or iEnumerable
    {
        // !!! sort by score


        // !!! order by Road_No (also sorts by Section_No and Start_Chain)
    }
}
