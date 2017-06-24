using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ReceiptProject2_6_22_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetData1();
        }

        //Allowing a public connection accessible to all columns
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-QUAEPTE;Initial Catalog=Receipts_DB;Integrated Security=True");
        SqlCommand command = new SqlCommand();
        SqlDataReader datapop1, datapop2, datapop3, eventdata1, eventdata2;

        string GroceryCoString, StoreAddress, StoreID;

        private class ListboxCategories
        {
            // public string GroceryCoString { get; set; }
            //public string StoreAddress { get; set; }
            //public string StoreID { get; set; }



            /*  public ListboxCategories(string grocerycostring,string storeaddress, string storeid)
              {
                  this.GroceryCoString = grocerycostring;
                  this.StoreAddress = storeaddress;
                  this.StoreID = storeid; 
              }*/

        }


        void SetData1() //This populates the first table
        {

            command.Connection = con;
            con.Open();
            command.CommandText = "Select Company_Name from Companies";
            datapop1 = command.ExecuteReader();
            if (datapop1.HasRows)
            {
                while (datapop1.Read())
                {
                    listBox1.Items.Add(datapop1[0]);
                }
            }
            con.Close();

            //  ListboxCategories ABC = new ListboxCategories(); 
            // listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
            //listBox2.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
        }


        void SetData2() //This populates the 2nd listbox
        {

            string GroceryID = GroceryCoString;
            command.Connection = con;
            con.Open();
            command.CommandText = "Select StreetAddress FROM Stores WHERE GroceryCompany_ID = '" + GroceryID + "'";
            //command.CommandText = "Select StreetAddress FROM Stores";
            datapop2 = command.ExecuteReader();
            if (datapop2.HasRows)
            {
                while (datapop2.Read())
                {
                    listBox2.Items.Add(datapop2[0]);
                }
            }
            con.Close();
            // listBox2.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);


            //StoreID = listBox2.SelectedIndex.ToString();
            //MessageBox.Show(StoreID); 
        }

        void SetData3()
        {
            command.Connection = con;
            con.Open();
            command.CommandText = "Select ItemName FROM Grocery_Inventory WHERE Store_ID = '" + StoreID + "'";
            //command.CommandText = "Select StreetAddress FROM Stores";
           datapop3 = command.ExecuteReader();
            if (datapop3.HasRows)
            {
                while (datapop3.Read())
                {
                    listBox3.Items.Add(datapop3[0]);
                }
            }
            con.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string GroceryName = listBox1.SelectedItem.ToString();
            con.Open();
            command.Connection = con;
            command.CommandText = "Select GroceryCompany_ID from Companies WHERE Company_Name = '" + GroceryName + "'";
            eventdata1 = command.ExecuteReader();
            eventdata1.Read();
            //Seeing if I can set private messages
            GroceryCoString = eventdata1[0].ToString();
            con.Close(); //closing the connection so a new one can be opened in SetData(2)
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            //Populate Data based off listbox1
            SetData2();
            //Even after calling SetData2, the code hit a deadend/loop here, even though there was a listbox2_selected index change. Had to force listbox event 2.
            //This is breaking because it forces the user to select something after the listbox2_selected index
            if (listBox2.SelectedIndex !=0 |listBox2.SelectedIndex !=-1)  
            {
                    listBox2.SelectedIndexChanged += new EventHandler(listBox2_SelectedIndexChanged);
            }
        }


        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

         //listbox2 selected item to string
                StoreAddress = listBox2.SelectedItem.ToString();
            
            con.Open();
            command.Connection = con;
            command.CommandText = "Select Store_ID from Stores WHERE StreetAddress = '" + StoreAddress + "'";
            eventdata2 = command.ExecuteReader();
            eventdata2.Read();
            StoreID = eventdata2[0].ToString();
            con.Close(); //closing the connection so a new one can be opened in SetData(2)
            listBox3.Items.Clear();
            SetData3();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            //when the form loads it opens the connection
            command.Connection = con;

            // TODO: This line of code loads data into the 'receipts_DBDataSet.Grocery_Inventory' table. You can move, or remove it, as needed.
            //this.grocery_InventoryTableAdapter.Fill(this.receipts_DBDataSet.Grocery_Inventory);

        }

    }
}
