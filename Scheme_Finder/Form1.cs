/********************************************************************************************************
 * 
 * Scheme_finder V1 re-written by Dean Findlay 19/01/14
 * 
 * v1 basic functionality - fixed scheme length drift
 * 
 * 19/01/14 v1.1 - added list functionality, need to add to listviews on form
 * 
 * 11/02/14 v1.2 - listviews working 
 * 
 * 11/02/14 v1.2.1 - Cleaned up methods from MainForm for readability and flow
 * 
 * 
 * TODO
 * 
 * Clean program up - abstraction + encapsulation
 * 
 * Colour scores in scheme view according to UKPMS value ranges
 * 
 * Create a continuous scheme workflow where if a scheme continues to be viable the extents are
 *    extended to accomodate  --- Use branch/fork in GITHub ---
 *    
 * Functionality to choose wich outputs are included
 * 
 * Ability to load in pre-analysed schemes
 * 
 * Scheme efficiency tools - STDEV...
 * 
 * Sort scheme summary on score, Road_NO/Section_No, STDEV
 * 
 * Choose Min/Max score range to highlight different typres of conditions
 * 
 * Ability to read in Un-processed SCANNER data and apply different weightings
 * 
 * Add timer to update status bar while
 * 
 * Add mapping portal using MapWinGIS
 * 
 * ****************************************************************************************************/
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

            // add columns to listviews
            initialiseListView(lvw_SchemeSummary, summaryHeaders);
            initialiseListView(lvw_Scheme, SchemeHeaders);            
        }

        // Internal variables
        private string  sourceFilePath, destinationFilePath;
        private float targetLength, targetScore, targetTolerance;

        // initialise list of sections
        List<Section> ListSections = new List<Section>();

        // initialise list of list of sections
        List<List<Section>> ListofListSections = new List<List<Section>>();

        // Initialise list of listviewitems to hold the scheme data
        List<ListViewItem> schemeSectionListView = new List<ListViewItem>();

        // columns to be added to the listviews
        string[] summaryHeaders = new string[] {"Scheme No", "Road No", "Start Section", "Start Chain"
                , "End Section", "End Chain", "Score"};

        string[] SchemeHeaders = new string[] {"ID", "Road No", "Section", "Lane"
                , "Start Chain", "End Chain", "Score"};
 

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Display 'About' information
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Scheme Finder V1.2.1\nWritten by Dean Findlay\nDean.Findlay@Derbyshire.Gov.UK");
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

            // Check both file paths have ben supplied
            if (checkFilePaths() == false)
            {
                return;
            }

            // Checks for valid numerical input and starts program running if true
            if (checkValidNumbers())
            {
                startFindSchemes();
            }
            else
            {
                return;
            }
        }

        // Starts the FindSchemes part of the program running
        private void startFindSchemes()
        {
            // clears the listofschemes so new schemes not added to a previous list
            // if program run twice
            ListofListSections.Clear();

            this.Text = "Scheme finder - Extracting";
            // Initate instance of FinndSchemes and start program
            FindSchemes run = new FindSchemes();
            run.StartFinder(sourceFilePath, destinationFilePath
                , targetLength, targetScore, targetTolerance
                , ListofListSections);

            // calls to start scheme analysis once FindSchemes returns
            startAnalysis();
        }

        public void startAnalysis()
        {
            this.Text = "Scheme analyser";
            // !!! display count while testing
            MessageBox.Show(ListofListSections.Count.ToString());

            // populate the scheme summary box
            populateSchemeSummary();
        }

        // Check that valid numbers have been supplied, if not user is prompted accordingly
        private bool checkValidNumbers()
        {
            if ((Helpers.validNumber(txt_Length.Text, ref targetLength, "length")) &&
                (Helpers.validNumber(txt_Score.Text, ref targetScore, "score")) &&
                (Helpers.validNumber(txt_Tolerance.Text, ref targetTolerance, "tolerance")))
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        // Checks to see if file paths have been specified, if not then user is prompted accordingly
        private bool checkFilePaths()
        {
            if (sourceFilePath == null)
            {
                MessageBox.Show("No source path specified");
                return false ;
            }
            if (destinationFilePath == null)
            {
                MessageBox.Show("No destination path specified");
                return false;
            }

            return true;
        }

        // initialise given listview with headers in string array
        private void initialiseListView(ListView lview, string[] fields)
        {
            // clear columns or it adds to any already present
            lview.Columns.Clear();

            foreach (string i in fields)
            {
                // !!! maybe change this to automatic width using '-2'
                lview.Columns.Add(i);
            }            
        }

        // populate the scheme summary listview
        private void populateSchemeSummary()
        {
            lvw_SchemeSummary.Items.Clear();

            // counter for Scheme No
            int i = 1;

            // iterate through each member of list
            foreach (List<Section> scheme in ListofListSections)
            {
                // summarise scheme - send 'i' to be added to beginning of string[]
                string[] tempString = ListSection.SummariseScheme(scheme, i);

                // create listViewItem
                ListViewItem tempLV = new ListViewItem(tempString);

                // send listViewItem to be displayed
                sendValues(lvw_SchemeSummary, tempLV);

                i++;
            }
        }

        private void sendValues(ListView lview, ListViewItem item)
        {
            lview.Items.Add(item);
        }

        // Show selected scheme from lvw_SchemeSummary in lvw_Scheme
        private void btn_ShowScheme_Click(object sender, EventArgs e)
        {
            // clear items or it adds to any present
            lvw_Scheme.Items.Clear();

            // finds the index of the selected schemeSummary listview
            // if no valid scheme is selected a value of -1 will be returned
            int lstIndex = checkIndex();
            // leaves call if no valid index selected
            if (lstIndex == -1)
            {
                return;
            }

            // temporary list<section> to hold scheme
            List<Section> tempScheme = new List<Section>();

            // populates tempScheme with the selected scheme
            // if no scheme is assigned the leave call
            if (!summaryToScheme(ref tempScheme, lstIndex))
            {
                return;
            }
            
            // populate schemeSectonListView
            populateSchemeSectionListView(tempScheme);
          

            // colour items in the list box
            // !!! change this to use the colour style of the UKPMS range

            // Colours the core values in the schemes listview box
            colourSchemeScores();

            // send each listviewitem to be displayed
            foreach (ListViewItem lv in schemeSectionListView)
            {
                sendValues(lvw_Scheme, lv);
            }
        }
        
        // Checks for selected index in the schemeSummary listview
        private int checkIndex()
        {
            // ensure one scheme is selected and find it's index            
            if (lvw_SchemeSummary.SelectedItems.Count == 1)
            {
                return lvw_SchemeSummary.SelectedItems[0].Index;
            }
            else if (lvw_SchemeSummary.SelectedItems.Count == 0)
            {
                MessageBox.Show("No scheme selected");
                return -1;
            }
            else
            {
                MessageBox.Show("Multiple schemes selected");
                return -1;
            }
        }

        // populates the tempScheme with the correct scheme
        private bool summaryToScheme(ref List<Section> tempScheme ,int lstIndex)
        {
            // populate tempScheme with the correct data
            System.Collections.IEnumerator en = ListofListSections.GetEnumerator();
            int i = 0;
            if (en.MoveNext())
            {
                while (lstIndex != i)
                {
                    en.MoveNext();
                    i++;
                }
                // find the selected scheme and cast to tempScheme holder
                tempScheme = (List<Section>)en.Current;

                return true;
            }
            else
            {
                return false;
            }
        }

        // populates SchemeSectionListView with listViewItems converted from tempScheme
        private void populateSchemeSectionListView(List<Section> tempScheme)
        {
            // Clear schemeSectionListViews of any existing sections
            if (schemeSectionListView.Count > 0)
            {
                schemeSectionListView.Clear();
            }
            // Fill SchemeSectionsListView with Sections converted to ListViewItems
            Section tempSection = new Section();
            System.Collections.IEnumerator en1 = tempScheme.GetEnumerator();
            while (en1.MoveNext())
            {
                tempSection = (Section)en1.Current;
                schemeSectionListView.Add(ListSection.ConvertSectionToListViewItem(tempSection));
            }
        }

        // Colours the score value
        // !!! change this to use the colour style of the UKPMS range
        private void colourSchemeScores()
        {
            ListViewItem tempLvItem = new ListViewItem();
            System.Collections.IEnumerator en2 = schemeSectionListView.GetEnumerator();
            while (en2.MoveNext())
            {
                tempLvItem = (ListViewItem)en2.Current;
                if (float.Parse(tempLvItem.SubItems[6].Text) >= 90)
                {
                    tempLvItem.UseItemStyleForSubItems = false;
                    tempLvItem.SubItems[6].ForeColor = Color.Red;
                }
                else
                {
                    tempLvItem.UseItemStyleForSubItems = false;
                    tempLvItem.SubItems[6].ForeColor = Color.Blue;
                }
            }
        }

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

    // Controls the flow of the program
    public class FindSchemes
    {
        // variables from form
        private string sourcePath, destinationPath;
        private float targetLength, targetScore, targetTolerance;

        // !!! variables to hold relevant information on the current scheme being processed
        // LaneA denotes the first lane that triggered the call while LaneB denotes the opposite side.
        private float laneAStartChain, laneAEndChain;

        // !!! Starting point for program
        public void StartFinder(string formSource, string formDestination
            , float formLength, float formScore, float formTol
            , List<List<Section>> listOfListSections)
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

                            // create list of sections from both lanes and add to list of list sections
                            listOfListSections.Add(makeList(queueSection, OppositeSideScheme));
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

            // close reader and writers so program can be run again if necessary
            reader.Close();
            Viable_SchemeWriter.Close();
            Opposite_SideWriter.Close();
            Both_SidesWriter.Close();

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

        // returns a list made from 2 queues of sections
        private List<Section> makeList(Queue<Section> laneA, Queue<Section> laneB)
        {
            List<Section> tempList = new List<Section>();

            foreach (Section secta in laneA)
            {
                tempList.Add(secta);
            }

            foreach (Section sectb in laneB)
            {
                tempList.Add(sectb);
            }

            return tempList;
        }
    }

    // Struct to hold Section + related tools
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

        // returns score of a section when given the length of the scheme
        public float ReturnScore(float schemeLen)
        {
            float score = this.score / schemeLen * this.length;

            return score;
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
        // summarise a list<Section> and return the road_No, Start and end Section_No,
        // start and end chain and score
        public static string[] SummariseScheme(List<Section> scheme, int i)
        {
            string road_no, start_section, end_section = "Unnasigned";
            float start_chain, end_chain = 999999, score;
            float laneA_Length, laneB_Length = 0;
            float laneA_Score, laneB_Score = 0;
            string mainLane;

            // read first section get road_no, start_section, start_chain
            List<Section>.Enumerator e1 = scheme.GetEnumerator();
            e1.MoveNext();
            Section firstSection = e1.Current;
            mainLane = firstSection.lane;
            road_no = firstSection.road_No;
            start_section = firstSection.section_No;
            start_chain = firstSection.start_Chain;
            // length taken to start off loop in next section
            laneA_Length = firstSection.length;

            // get lane A&B lengths
            while (e1.MoveNext())
            {
                Section tempSection = e1.Current;

                if (tempSection.lane == mainLane)
                {
                    laneA_Length += tempSection.length;
                }
                else
                {
                    laneB_Length += tempSection.length;
                }
            }


            // read every other section (while Lane same overwriting end_section, end_Chain)
            // get score
            List<Section>.Enumerator e2 = scheme.GetEnumerator();
            e2.MoveNext();
            firstSection = e2.Current;
            laneA_Score = firstSection.ReturnScore(laneA_Length);
            while (e2.MoveNext())
            {
                Section tempSection = e2.Current;

                // overwrite end_section & end_Chain
                if (tempSection.lane == mainLane)
                {
                    end_section = tempSection.section_No;
                    end_chain = tempSection.end_Chain;

                    laneA_Score += tempSection.ReturnScore(laneA_Length);
                }
                else
                {
                    laneB_Score += tempSection.ReturnScore(laneB_Length);
                }
            }


            // divide each lane by 2 and add together
            score = (laneA_Score / 2) + (laneB_Score / 2);

            string[] temp = new string[] {i.ToString(), road_no, start_section, start_chain.ToString()
                , end_section, end_chain.ToString(), score.ToString()};
            return temp;
        }

        // Convert a Section to a listViewItem
        public static ListViewItem ConvertSectionToListViewItem(Section section)
        {
            string id = section.id.ToString();
            string roadNo = section.road_No;
            string sectionNo = section.section_No.ToString();
            string lane = section.lane;
            string startCh = section.start_Chain.ToString();
            string endCh = section.end_Chain.ToString();
            string score = section.score.ToString();

            ListViewItem holder = new ListViewItem(new[] {id, roadNo,
                sectionNo, lane, startCh, endCh, score});

            return holder;
        }
    }

    // !!! tools for working with list<list<Section>>'s
    public static class ListListSection // may have to add interface here such as iSortable or iEnumerable
    {

        // !!! sort by score


        // !!! order by Road_No (also sorts by Section_No and Start_Chain)
    }
}
