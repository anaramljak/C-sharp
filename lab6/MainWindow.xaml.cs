using Lab6.Validators;
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Windows;

namespace Lab6
{
    public partial class MainWindow : Window
    {
        BindingList<string> errors = new BindingList<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            errors.Clear();

            var slip = new PaymentSlip();
            slip.PayerName = Payer.Text;
            slip.RecipientName = Recipient.Text;
            slip.Currency = Currency.Text;
            slip.Total = Total.Text;
            slip.PayerIBAN = PayerIBAN.Text;
            slip.PayerModel = PayerModel.Text;
            slip.PayerNumber = PayerNumber.Text;
            slip.RecipientIBAN = RecipientIBAN.Text;
            slip.RecipientModel = RecipientModel.Text;
            slip.RecipientNumber = RecipientNumber.Text;
            slip.PurposeCode = PurposeCode.Text;
            slip.PaymentDescription = PaymentDescription.Text;
            try
            {
                slip.Date = (DateTime)Date.SelectedDate;
            }
            catch
            {
                errors.Add("Pick a Date");
            }


            if ((bool)Emergency.IsChecked)
                slip.Emergency = "Yes";
            else
                slip.Emergency = "No";

            SlipValidator validator = new SlipValidator();

            var results = validator.Validate(slip);

            if (results.IsValid == false)
            {
                foreach (var failure in results.Errors)
                {
                    errors.Add($"{failure.ErrorMessage}");
                }
            }

            if (errors.Count != 0)
            {
                var temp = "";
                foreach (var item in errors)
                    temp += " " + item + "\n";
                MessageBox.Show($"{temp}");
            }
            else
            {
                MessageBox.Show("Payment Completed!");
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DT User2\Desktop\Sixth\vjezba6\Lab6\Validators\Database.mdf;Integrated Security=True");
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into Payer values (@Name, @IBAN, @Model, @Number, @Total)", con);

                cmd.Parameters.AddWithValue("@Name", slip.PayerName);
                cmd.Parameters.AddWithValue("@IBAN", slip.PayerIBAN);
                cmd.Parameters.AddWithValue("@Model", slip.PayerModel);
                cmd.Parameters.AddWithValue("@Number", slip.PayerNumber);
                cmd.Parameters.AddWithValue("@Total", float.Parse(slip.Total));


                SqlCommand cmd2 = new SqlCommand("insert into Recipient values (@Name, @IBAN, @Model, @Number, @Total)", con);

                cmd2.Parameters.AddWithValue("@Name", slip.RecipientName);
                cmd2.Parameters.AddWithValue("@IBAN", slip.RecipientIBAN);
                cmd2.Parameters.AddWithValue("@Model", slip.RecipientModel);
                cmd2.Parameters.AddWithValue("@Number", slip.RecipientNumber);
                cmd2.Parameters.AddWithValue("@Total", float.Parse(slip.Total));

                SqlCommand cmd3 = new SqlCommand("insert into PaymentInfo values (@Currency, @Date , @Description, @PurposeCode, @Emergency, @Payer, @Recipient)", con);

                cmd3.Parameters.AddWithValue("@Currency", slip.Currency);
                cmd3.Parameters.AddWithValue("@Date", slip.Date.ToShortDateString());
                cmd3.Parameters.AddWithValue("@Description", slip.PaymentDescription);
                cmd3.Parameters.AddWithValue("@PurposeCode", slip.PurposeCode);
                cmd3.Parameters.AddWithValue("@Emergency", slip.Emergency);
                cmd3.Parameters.AddWithValue("@Payer", slip.PayerName);
                cmd3.Parameters.AddWithValue("@Recipient", slip.RecipientName);

                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();


                con.Close();

                Payer.Text = "";
                Recipient.Text = "";
                Currency.Text = "";
                Total.Text = "";
                Payer.Text = "";
                PayerIBAN.Text = "";
                PayerModel.Text = "";
                PayerNumber.Text = "";
                Recipient.Text = "";
                RecipientIBAN.Text = "";
                RecipientModel.Text = "";
                RecipientNumber.Text = "";
                PurposeCode.Text = "";
                PaymentDescription.Text = "";
                Emergency.IsChecked = false;
                Date.Text = "";
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            float totalPayed = 0;
            float totalIncome = 0;
            string currency = "";
            int timesPayed = 0;
            int timesRecieved = 0;

            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DT User2\Desktop\Sixth\vjezba6\Lab6\Validators\Database.mdf;Integrated Security=True");
            con.Open();



            SqlCommand GetPayerTotal = new SqlCommand("Select Total from Payer where Name =@Name", con);
            GetPayerTotal.Parameters.AddWithValue("@Name", Search.Text);

            SqlDataReader da = GetPayerTotal.ExecuteReader();
            while (da.Read())
            {
                for (int i = 0; i < da.FieldCount; i++)
                {
                    totalPayed += float.Parse(da[i].ToString());
                    timesPayed++;
                }
            }
            con.Close();
            con.Open();


            SqlCommand GetRecipientTotal = new SqlCommand("Select Total from Recipient where Name =@Name", con);
            GetRecipientTotal.Parameters.AddWithValue("@Name", Search.Text);

            SqlDataReader da2 = GetRecipientTotal.ExecuteReader();
            while (da2.Read())
            {
                for (int i = 0; i < da2.FieldCount; i++)
                {
                    totalIncome += float.Parse(da2[i].ToString());
                    timesRecieved++;
                }
            }
            con.Close();
            con.Open();


            SqlCommand GetCurrency = new SqlCommand("Select Currency from PaymentInfo where Payer =@Payer OR Recipient =@Payer", con);
            GetCurrency.Parameters.AddWithValue("@Payer", Search.Text);

            SqlDataReader da3 = GetCurrency.ExecuteReader();
            while(da3.Read())
                currency = da3.GetValue(0).ToString();
            
      
            MessageBox.Show($"{Search.Text} stanje računa : {(totalIncome - totalPayed).ToString()} {currency} \n" +
                $"Broj uplata : {timesRecieved}\n" +
                $"Broj isplata : {timesPayed}");
            con.Close();

            Search.Text = "";
        }
    }
}
