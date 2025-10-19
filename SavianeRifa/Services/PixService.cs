using System;
using System.IO;
using System.Text;
using System.Globalization;
using QRCoder;

namespace SavianeRifa.Services
{
    public class Payload
    {
        private string Nome { get; set; }
        private string ChavePix { get; set; }
        private string Valor { get; set; }
        private string Cidade { get; set; }
        private string TxtId { get; set; }
        private string DiretorioQrCode { get; set; }

        private string PayloadFormat = "000201";
        private string MerchantAccount;
        private string MerchantCategCode = "52040000";
        private string TransactionCurrency = "5303986";
        private string TransactionAmount;
        private string CountryCode = "5802BR";
        private string MerchantName;
        private string MerchantCity;
        private string AddDataField;
        private string Crc16 = "6304";

        public string PixCopiaCola => PayloadCompleta;

        private string PayloadCompleta = "";

        public Payload(string nome, string chavepix, string valor, string cidade, string txtId, string diretorio = "")
        {
            Nome = nome;
            ChavePix = chavepix;
            Valor = valor.Replace(",", "."); // normaliza vírgula para ponto
            Cidade = cidade;
            TxtId = txtId;
            DiretorioQrCode = string.IsNullOrEmpty(diretorio) ? Directory.GetCurrentDirectory() : diretorio;

            // Montagem dos campos
            string merchantAccountTam = $"0014BR.GOV.BCB.PIX01{ChavePix.Length:00}{ChavePix}";
            MerchantAccount = $"26{merchantAccountTam.Length:00}{merchantAccountTam}";

            // Corrigido: formata valor com ponto decimal e calcula tamanho corretamente
            double valorDouble = double.Parse(Valor, CultureInfo.InvariantCulture);
            string valorFormatado = valorDouble.ToString("0.00", CultureInfo.InvariantCulture);
            string transactionAmountTam = $"{valorFormatado.Length:00}{valorFormatado}";
            TransactionAmount = $"54{transactionAmountTam}";

            string addDataFieldTam = $"05{TxtId.Length:00}{TxtId}";
            AddDataField = $"62{addDataFieldTam.Length:00}{addDataFieldTam}";

            MerchantName = $"59{Nome.Length:00}{Nome}";
            MerchantCity = $"60{Cidade.Length:00}{Cidade}";
        }

        public void GerarPayload()
        {
            string payload = $"{PayloadFormat}{MerchantAccount}{MerchantCategCode}{TransactionCurrency}{TransactionAmount}{CountryCode}{MerchantName}{MerchantCity}{AddDataField}{Crc16}";
            string crc16Code = CalcularCRC16(payload);
            PayloadCompleta = $"{payload}{crc16Code}";

            GerarQrCode(PayloadCompleta, DiretorioQrCode);
            Console.WriteLine(PayloadCompleta);
        }

        private string CalcularCRC16(string payload)
        {
            ushort polynomial = 0x1021;
            ushort crc = 0xFFFF;

            byte[] bytes = Encoding.UTF8.GetBytes(payload);
            foreach (byte b in bytes)
            {
                crc ^= (ushort)(b << 8);
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x8000) != 0)
                        crc = (ushort)((crc << 1) ^ polynomial);
                    else
                        crc <<= 1;
                }
            }

            return crc.ToString("X4");
        }

        private void GerarQrCode(string payload, string diretorio)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
                using (QRCode qrCode = new QRCode(qrCodeData))
                using (var qrCodeImage = qrCode.GetGraphic(20))
                {
                    string path = Path.Combine(diretorio, "pixqrcodegen.png");
                    qrCodeImage.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }
    }
}
