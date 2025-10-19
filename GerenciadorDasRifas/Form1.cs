using Microsoft.EntityFrameworkCore;
using SavianeRifa.Data;
using SavianeRifa.Models;

namespace GerenciadorDasRifas
{
    public partial class Form1 : Form
    {
        AppDbContext db;

        PaymentInformation paymentInformation;

        PaymentInformation CurrentPayment { get; set; }
        int currentpaymentindex = 0;

        Rifa CurrentRifa { get; set; }
        int currentrifa = 0;

        public Form1()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-8I4A0RA\\ANDREY;Database=SavianeRifa;User Id=sa;Password=yerdna15043733;TrustServerCertificate=True;");

            db = new AppDbContext(optionsBuilder.Options);

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadInfo();
        }

        void LoadInfo()
        {
            img.SizeMode = PictureBoxSizeMode.Zoom;
            var compradores = db.PaymentInformations.Where(p => p.Rifas.Any(r => r.Status.ToUpper() == "RESERVADA")).ToList();
            lbPendings.DataSource = compradores;
            lbPendings.DisplayMember = "Name"; // Exibe o nome no ListBox
            lbPendings.SelectionMode = SelectionMode.One; // Apenas um item pode ser selecionado
        }

        private void lbPendings_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbPendings.SelectedItem != null)
            {
                CurrentPayment = (PaymentInformation)lbPendings.SelectedItem;

                lblNome.Text = CurrentPayment.Name;
                lblemail.Text = CurrentPayment.Email;
                lblphone.Text = CurrentPayment.PhoneNumber;
                lbldata.Text = CurrentPayment.RegisteredAt.ToString();
                lbltotal.Text = CurrentPayment.Amount.ToString("C");
                lblRifas.DataSource = db.Rifas.Where(r => r.PaymentInformationId == CurrentPayment.Id).ToList();
                lblRifas.DisplayMember = "Number";
                lblRifas.SelectionMode = SelectionMode.One;
                currentpaymentindex = lbPendings.SelectedIndex;
                string path = CurrentPayment.ComprovantePath;

                if (File.Exists(path) && Path.GetExtension(path).ToLower() is ".jpg" or ".jpeg" or ".png")
                {
                    using var tempStream = new MemoryStream(File.ReadAllBytes(path));
                    img.Image = Image.FromStream(tempStream);
                }
                else
                {
                    img.Image = Image.FromFile(@"C:\Users\Andrey Lindo\Pictures\fundos_convites\andrey\f3.png");
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void lblRifas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblRifas.SelectedItem != null)
            {
                CurrentRifa = (Rifa)lblRifas.SelectedItem;
                lblrifa.Text = CurrentRifa.Number;
                lblstatus.Text = CurrentRifa.Status;

                btnvendido.Enabled = CurrentRifa.Status == "Reservada";
            }
            currentrifa = lblRifas.SelectedIndex;
        }

        private void btnvendido_Click(object sender, EventArgs e)
        {
            if (CurrentRifa != null)
            {
                var rifa = db.Rifas.FirstOrDefault(r => r.Id == CurrentRifa.Id);
                if (rifa != null)
                {
                    rifa.Status = "Vendida";
                    db.SaveChanges();

                    lblstatus.Text = "Vendida";
                    CurrentRifa = rifa;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadInfo();
        }
    }
}
