using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airway.Entity;
using System.Windows.Forms;
namespace Airway.Data
{
    public class DataBaseConnection
    {
        SqlConnection connector;
        SqlCommand command;
        SqlDataReader reader;

        public DataBaseConnection()
        {
            connector = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\study\AIRWAYBUNIFU\AIRWAYBUNIFU\Airway.Data\airwaydata.mdf;Integrated Security=True;Connect Timeout=30");
        }

        public List<Plane> getPlane(string quary)
        {
            List<Plane> listOfPlane = new List<Plane>();
            try
            {
                connector.Open();
                command = new SqlCommand(quary, connector);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read() == true)
                    {
                        string planeID = (string)reader["Planeid"];
                        string planeName = (string)reader["planeName"];
                        string destination = (string)reader["destination"];
                        string start = (string)reader["start"];
                        DateTime departingDate = (DateTime)reader["departingDate"];
                        Byte maximumBusinessClass = (Byte)reader["maximumBusinessClass"];
                        Byte maximumEconomicClass = (Byte)reader["maximumEconomicClass"];
                        Byte maximumDeluxClass = (Byte)reader["maximumDeluxClass"];
                        double BusinessTicketPrice = (double)reader["BusinessTicketPrice"];
                        double EconomicTicketPrice = (double)reader["EconomicTicketPrice"];
                        double DeluxTicketPrice = (double)reader["DeluxTicketPrice"];
                        Byte CountBusinessClass = (Byte)reader["CountBusinessClass"];
                        Byte CountEconomicClass = (Byte)reader["CountEconomicClass"];
                        Byte CountDeluxClass = (Byte)reader["CountDeluxClass"];
                        listOfPlane.Add(new Plane(planeID, planeName, destination, start, departingDate,
                                      maximumBusinessClass, maximumEconomicClass, maximumDeluxClass,
                                      BusinessTicketPrice, EconomicTicketPrice,
                                      DeluxTicketPrice, CountBusinessClass, CountEconomicClass, CountDeluxClass));

                    }
                }
                connector.Close();
                reader.Close();
            }
            catch (Exception e) { MessageBox.Show("Error in Database get Plane \n" + e.ToString()); }

            return listOfPlane;
        }

        public List<Passenger> getPassenger(string quary)
        {
            List<Passenger> listOfPassenger = new List<Passenger>();
            try
            {
                connector.Open();
                command = new SqlCommand(quary, connector);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read() == true)
                    {
                        string name = (string)reader["name"];
                        string contactNo = (string)reader["contact"];
                        string passportNo = (string)reader["passport"];
                        string email = (string)reader["email"];
                        string ticketID = (string)reader["ticketID"];
                        byte seatBooked = (byte)reader["seatBooked"];
                        DateTime date=(DateTime)reader["DepartingDate"];
                        string starting = (string)reader["Starting"];
                        string destination = (string)reader["Destination"];
                        listOfPassenger.Add(new Passenger(name, contactNo, passportNo, email, ticketID, seatBooked,date,starting,destination));
                    }
                }
                connector.Close();
                reader.Close();
            }
            catch (Exception e) { MessageBox.Show("Error in Database get Passenger \n" + e.ToString()); }

            return listOfPassenger;
        }

        public void executeQuarry(string quary)
        {
            try
            {
                command = new SqlCommand(quary, connector);
                connector.Open();
                command.ExecuteNonQuery();
                connector.Close();
            }
            catch (Exception e) { MessageBox.Show("Error in executeQuarry\n" + quary + e.ToString()); }
        }


        public List<bool> getSeatData(string quary, int row, char endChar, char seatClass)
        {
            List<bool> listOfSeat = new List<bool>();
            try
            {
                connector.Open();
                command = new SqlCommand(quary, connector);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read() == true)
                    {
                        char a = 'A';
                        int count = 1;
                        while (count <= row && a < endChar)
                        {
                            string columnName = seatClass.ToString() + a + count;
                            bool result = (bool)reader[columnName];
                            listOfSeat.Add(result);
                            if (count == row)
                            {
                                a = (char)(a + 1);
                                count = 0;
                            }
                            count++;
                        }
                    }
                }
                connector.Close();
                reader.Close();
            }
            catch (Exception e) { MessageBox.Show("Error in Database get Seat \n" + e.ToString()); }

            return listOfSeat;
        }


        public AutoCompleteStringCollection getName(string quary)
        {
            AutoCompleteStringCollection listOfPlase = new AutoCompleteStringCollection();
            try
            {
                connector.Open();
                command = new SqlCommand(quary, connector);
                reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read() == true)
                    {
                        listOfPlase.Add((string)reader[0]);
                    }
                }
                connector.Close();
                reader.Close();
            }
            catch (Exception e) { MessageBox.Show("Error in Database get Name \n" + e.ToString()); }
            return listOfPlase;
        }
    }

}
