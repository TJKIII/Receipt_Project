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
        SqlDataReader datapop1,datapop2, datapop3, eventdata1, eventdata2;

        string GroceryCoString, StoreAddress, StoreID;

        

       private void SetData1() //This populates the first table
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
            }

        private void SetData2(string GroceryID) //This populates the first table
        {
            //listBox2.Items.Clear();
            command.Connection = con;
            con.Open();
            command.CommandText = "Select StreetAddress FROM Stores WHERE GroceryCompany_ID = '" + GroceryID + "'";
           // command.CommandText = "Select StreetAddress FROM Stores";
            datapop2 = command.ExecuteReader();
            if (datapop2.HasRows)
            {
                while (datapop2.Read())
                {
                    listBox2.Items.Add(datapop2[0]);
                }
            }
            con.Close(); 

            //string StoreAdress = listBox2.SelectedIndex.ToString();
          // MessageBox.Show(StoreAddress);

            
        }

        private void SetData3(string StoreID)
        {
            // MessageBox.Show(StoreID);
         
            
        }


        

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string GroceryName = listBox1.SelectedItem.ToString();
            con.Open();
            command.Connection = con; 
            command.CommandText = "Select GroceryCompany_ID from Companies WHERE Company_Name = '"+GroceryName+"'";
            eventdata1 = command.ExecuteReader();
            eventdata1.Read();
            GroceryCoString = eventdata1[0].ToString();
            con.Close(); //closing the connection so a new one can be opened in SetData(2)
            listBox2.Items.Clear();
            SetData2(GroceryCoString); 

        }

        private void listBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            MessageBox.Show("LOL IS THIS THING ON?");
                /*
            string StoreAddress = listBox2.SelectedItem.ToString();
            MessageBox.Show(StoreAddress);
           
              con.Open();
                command.Connection = con;
                command.CommandText = "Select Store_ID from Stores WHERE StreetAddress = '" + StoreAddress + "'";
                eventdata2 = command.ExecuteReader();
                eventdata2.Read();
                StoreID = eventdata2[0].ToString();
                con.Close(); //closing the connection so a new one can be opened in SetData(2)

            MessageBox.Show(StoreID);

            SetData3(StoreID);
*/
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
