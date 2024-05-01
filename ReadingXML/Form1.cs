using System;
using System.Windows.Forms;
using System.Xml;

namespace ReadingXML
{
    public partial class Form1 : Form
    {
        private ListView listView;
        private Button btnLoadData;

        public Form1() 
        {
            InitializeComponent();


            listView = new ListView
            {
                Width = 800,  
                Height = 600, 
                View = View.Details,
                FullRowSelect = true
            };

            
            listView = new ListView
            {
                Dock = DockStyle.Top,
                View = View.Details,
                FullRowSelect = true
            };

            
            listView.Columns.Add("Título", 150);
            listView.Columns.Add("Artista", 150);
            listView.Columns.Add("País", 100);
            listView.Columns.Add("Precio", 70);
            listView.Columns.Add("Año", 70);

            btnLoadData = new Button
            {
                Text = "Cargar Datos",
                Dock = DockStyle.Bottom
            };

            btnLoadData.Click += (s, e) => LoadData();

            Controls.Add(listView);
            Controls.Add(btnLoadData);
        }

        private void LoadData()
        {
            listView.Items.Clear(); 

            string url = "https://www.w3schools.com/xml/cd_catalog.xml";

            using (XmlTextReader reader = new XmlTextReader(url))
            {
                string title = "";
                string artist = "";
                string country = "";
                string price = "";
                string year = "";

                
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "TITLE":
                                reader.Read();
                                title = reader.Value;
                                break;
                            case "ARTIST":
                                reader.Read();
                                artist = reader.Value;
                                break;
                            case "COUNTRY":
                                reader.Read();
                                country = reader.Value;
                                break;
                            case "PRICE":
                                reader.Read();
                                price = reader.Value;
                                break;
                            case "YEAR":
                                reader.Read();
                                year = reader.Value;
                                break;
                        }
                    }

                    
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "CD")
                    {
                        var listViewItem = new ListViewItem(title);
                        listViewItem.SubItems.Add(artist);
                        listViewItem.SubItems.Add(country);
                        listViewItem.SubItems.Add(price);
                        listViewItem.SubItems.Add(year);

                        listView.Items.Add(listViewItem);

                        title = "";
                        artist = "";
                        country = "";
                        price = "";
                        year = "";
                    }
                }
            }
        }
    }
}
