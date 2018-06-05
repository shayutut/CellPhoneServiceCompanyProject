using CRM.CommonFiles.Models;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace CRM.BL
{
    public class ReportManager
    {
        public string WriteReportOfClient(Client client, List<Call> calls, List<SMS> smss, Line line, ClientType clientType, Package package)
        {
            string p;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Payment for mr. {client.Name} {client.LastName}^Line Number: {line.Number}^");
            double smssPay = clientType.SMSPrice * smss.Count;
            double callsPay = 0;
            double Dureation = 0;
            for (int j = 0; j < calls.Count; j++)
                Dureation += calls[j].Dureation;
            var familyTalk = Family(calls, client, line);
            p = TheBestPackage(Dureation, smss.Count, familyTalk, clientType.MinutePrice);
            switch (package.Name)
            {
                case "200With100": Dureation = With100(Dureation); break;
                case "70With50": Dureation = With50(Dureation); break;
                case "400With160": Dureation = With160(Dureation); break;
                case "Family": Dureation -= familyTalk; break;
                case "HappyThreeFriends": HappyThreeFriends(); ; break;
                case "FavoriteNumber": FavoriteNumber(); break;
                case "MeetingMan": MeetingMan(); break;
                default:
                    break;
            }
            callsPay = Dureation * clientType.MinutePrice;
            if (Dureation < 0) Dureation = 0;
            stringBuilder.AppendLine($"send {smss.Count} SMS's-{smssPay}$^Talked {Dureation} minuts-{callsPay}$^");
            stringBuilder.AppendLine($"For Package {package.Name} {package.PackageTotalPrice}$");
            stringBuilder.AppendLine($"{package.PackageTotalPrice + smssPay + callsPay}$ for total");
            #region oldWork
            //for (int i = 0; i < lines.Count; i++)
            //{
            //double[] smssPay = new double[lines.Count];
            //double[] callsPay = new double[lines.Count];
            //double Dureation;
            //    List<SMS> slist = smss.Where(s => s.LineId == lines[i].Id).ToList();
            //    smssPay[i] = slist.Count * clientType.SMSPrice;
            //    List<Call> clist = calls.Where(c => c.LineId == lines[i].Id).ToList();
            //    for (int j = 0; j < clist.Count; j++)
            //    {
            //        callsPay[i] = clist[j].Dureation * clientType.MinutePrice;
            //        Dureation += clist[j].Dureation;
            //    }
            //    stringBuilder.AppendLine($"Line number:{lines[i].Number}^send {slist.Count} SMS's-{smssPay[i]}$^Talked {Dureation} minuts-{callsPay[i]}$");
            //}
            #endregion

            var date = DateTime.Now.ToShortDateString();
            date = date.Replace("/", ".");

            string path = $"../{client.Name}{date}.pdf";
            //File.WriteAllText(path, $"{stringBuilder.ToString()}");
            PDFReport(path, $"{client.Name}{date}", stringBuilder.ToString());

            return p;
        }

        private string TheBestPackage(double dureation, int smss, double familyTalk, double callPay)
        {
            double dureationWith160 = With160(dureation), dureationWith50 = With50(dureation), dureationWith100 = With100(dureation), dureationWithOutFamilyTalk200 = dureation - familyTalk;//, dureation5 = dureation, dureation6 = dureation;
            List<double> pays = new List<double>() { (dureationWith160 * callPay) + 160, (dureationWith50 * callPay) + 50, (dureationWith100 * callPay) + 100, (dureationWithOutFamilyTalk200 * callPay) + 200 };
            int min = pays.FindIndex(l => l == pays.Min());
            switch (min)
            {
                case 0: return "400With160"; break;
                case 1: return "70With50"; break;
                case 2: return "200With100"; break;
                case 3: return "Family"; break;
                default:
                    return "";
                    break;
            }
        }

        private double With160(double dureation)
        {
            if (dureation < 400)
            {
                return 0;
            }
            return (dureation - 400);
        }

        private double With50(double dureation)
        {
            if (dureation < 70)
            {
                return 0;
            }
            return (dureation - 70);
        }

        private double With100(double dureation)
        {
            if (dureation < 200)
            {
                return 0;
            }
            return (dureation - 200);
        }

        private double Family(List<Call> calls, Client client, Line line)
        {
            var cls = client.Lines.ToList();
            double dureation = 0;
            for (int j = 0; j < calls.Count; j++)
            {
                for (int i = 0; i < client.Lines.Count; i++)
                {
                    if (cls[i].Number == calls[j].DestinaionNumber)
                        dureation += calls[j].Dureation;
                    break;
                }
            }
            return dureation;
        }


        private void MeetingMan()
        {
        }

        private void FavoriteNumber()
        {
        }

        private void HappyThreeFriends()
        {
        }
        public void PDFReport(string path, string fileName, string content)
        {
            try
            {
                content = content.Replace("^", Environment.NewLine);
                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = fileName;
                var p = new PdfPage();
                pdf.AddPage(p);
                XGraphics graphics = XGraphics.FromPdfPage(p);
                XTextFormatter textFormatter = new XTextFormatter(graphics);
                textFormatter.DrawString(content, new XFont("Verdana", 20), XBrushes.Black, new XRect(10, 10, p.Width, p.Height), XStringFormat.TopLeft);
                pdf.Save(path);
                pdf.Dispose();
                Process.Start(Path.GetFullPath(path));
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
