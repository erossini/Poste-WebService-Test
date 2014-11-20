using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WSTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string username = "NPSP0001";           // credenziali test Orwell
        string password = "Orwell2014";
        string membercode = "1";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendTransactions.BlankVerificationDTO bv = new SendTransactions.BlankVerificationDTO();
            bv.AccountHolder = "CONDOMINIO VIA DEI GLICINI";
            bv.AccountNum = "87155008";
            bv.Amount = (decimal)10.10;
            bv.PaymentType = SendTransactions.PaymentType.Cash;
            bv.Type = SendTransactions.PostalSlipModel.TD123;
            bv.Reason = "CAUSALE PROVA";

            SendTransactions.PayerDTO py = new SendTransactions.PayerDTO();
            py.Address = "Indirizzo";
            py.City = "Roma";
            py.Name = "MASOTTI PASQUALE";
            py.PostalCode = "00100";
            py.StateOrProvince = "RM";
            bv.Payer = py;

            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.PaymentVerifiedDTO pvdto = stsc.SendFirstRequestBollettinoBianco(username, password, membercode, bv);
            if (pvdto.Success)
            {
                this.textBlockTransBlank.Text = pvdto.TransactionID;
                this.btnConfirmBlank.IsEnabled = true;
                this.btnCancelBlank.IsEnabled = true;
                this.btnRevalseBlank.IsEnabled = true;
            }
            else
            {
                this.textBlockTransBlank.Text = "";
                this.textBlockError.Text = pvdto.Code + " " + pvdto.Message;
                this.btnConfirmBlank.IsEnabled = false;
                this.btnCancelBlank.IsEnabled = false;
                this.btnRevalseBlank.IsEnabled = false;
            }
            stsc.Close();
            pvdto = null;
            bv = null;
        }

        private void btnConfirmBlank_Click(object sender, RoutedEventArgs e)
        {
            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.PaidSlipDTO psdto = stsc.SendConfirmBollettinoBianco(username, password, membercode, this.textBlockTransBlank.Text);

            if (psdto.Success)
            {
                this.textBlockConfirmBlank.Text = "Ok";
                this.btnConfirmPrefilled.IsEnabled = true;
                this.btnCancelPrefilled.IsEnabled = true;
                this.btnRevalsePrefilled.IsEnabled = true;
            }
            else
            {
                this.textBlockConfirmBlank.Text = "";
                this.textBlockError.Text = psdto.Code + " " + psdto.Message;
                this.btnConfirmPrefilled.IsEnabled = false;
                this.btnCancelPrefilled.IsEnabled = false;
                this.btnRevalsePrefilled.IsEnabled = false;
            }
        }

        private void btnCancelBlank_Click(object sender, RoutedEventArgs e)
        {
            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.CancelledPaymentDTO psdto = stsc.SendCancelBollettinoBianco(username, password, membercode, this.textBlockTransBlank.Text);

            if (psdto.Success)
            {
                this.textBlockCancelBlank.Text = "Ok";
            }
            else
            {
                this.textBlockCancelBlank.Text = "";
                this.textBlockError.Text = psdto.Code + " " + psdto.Message;
            }
        }

        private void btnRevalseBlank_Click(object sender, RoutedEventArgs e)
        {
            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.ReversedPaymentDTO psdto = stsc.SendReverseBollettinoBianco(username, password, membercode, this.textBlockTransBlank.Text);

            if (psdto.Success)
            {
                this.textBlockRevalseBlank.Text = "Ok";
            }
            else
            {
                this.textBlockRevalseBlank.Text = "";
                this.textBlockError.Text = psdto.Code + " " + psdto.Message;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SendTransactions.PrefilledVerificationDTO bv = new SendTransactions.PrefilledVerificationDTO();
            bv.AccountNum = "1404";
            bv.Amount = (decimal)10.10;
            bv.PaymentType = SendTransactions.PaymentType.Cash;
            bv.PaymentSlipNum = "420093200623621607";
            bv.Type = SendTransactions.PostalSlipModel.TD896;

            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.PaymentVerifiedDTO pvdto = stsc.SendFirstRequestBollettinoPrefilled(username, password, membercode, bv);
            if (pvdto.Success)
            {
                this.textBlockTransPrefilled.Text = pvdto.TransactionID;
                this.btnConfirmPrefilled.IsEnabled = true;
                this.btnCancelPrefilled.IsEnabled = true;
                this.btnRevalsePrefilled.IsEnabled = true;
            }
            else
            {
                this.textBlockTransPrefilled.Text = "";
                this.textBlockError.Text = pvdto.Code + " " + pvdto.Message;
                this.btnConfirmPrefilled.IsEnabled = false;
                this.btnCancelPrefilled.IsEnabled = false;
                this.btnRevalsePrefilled.IsEnabled = false;
            }
            stsc.Close();
            pvdto = null;
            bv = null;
        }

        private void btnConfirmPrefilled_Click(object sender, RoutedEventArgs e)
        {
            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.PaidSlipDTO psdto = stsc.SendConfirmBollettinoPrefilled(username, password, membercode, this.textBlockTransPrefilled.Text);

            if (psdto.Success)
            {
                this.textBlockConfirmPrefilled.Text = "Ok";
                this.btnRevalsePrefilled.IsEnabled = true;
            }
            else
            {
                this.textBlockConfirmPrefilled.Text = "";
                this.textBlockError.Text = psdto.Code + " " + psdto.Message;
                this.btnRevalsePrefilled.IsEnabled = false;
            }
        }

        private void btnCancelPrefilled_Click(object sender, RoutedEventArgs e)
        {
            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.CancelledPaymentDTO psdto = stsc.SendCancelBollettinoPrefilled(username, password, membercode, this.textBlockTransPrefilled.Text);

            if (psdto.Success)
            {
                this.textBlockCancelPrefilled.Text = "Ok";
            }
            else
            {
                this.textBlockCancelPrefilled.Text = "";
                this.textBlockError.Text = psdto.Code + " " + psdto.Message;
            }
        }

        private void btnRevalsePrefilled_Click(object sender, RoutedEventArgs e)
        {
            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.ReversedPaymentDTO psdto = stsc.SendReverseBollettinoPrefilled(username, password, membercode, this.textBlockTransBlank.Text);

            if (psdto.Success)
            {
                this.textBlockRevalsePrefilled.Text = "Ok";
            }
            else
            {
                this.textBlockRevalsePrefilled.Text = "";
                this.textBlockError.Text = psdto.Code + " " + psdto.Message;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SendTransactions.VerifyUserDTO bv = new SendTransactions.VerifyUserDTO();
            bv.CardNumber = "5299489000000715";
            SendTransactions.CardHolderDTO ch = new SendTransactions.CardHolderDTO();
            ch.FirstName = "ANTONELLA";
            ch.LastName = "DE BLASIO";
            ch.TaxIdCode = "DBLNNL78E55H501Y";
            bv.CardHolder = ch;

            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.ConfirmPostePayIPPS pvdto = stsc.SendFirstRequestPostePay(username, password, membercode, bv);
            if (pvdto.Success)
            {
                this.textBlockTransPostePay.Text = pvdto.RequestId;
                this.btnCancelPostePay.IsEnabled = true;
                this.btnConfirmPostePay.IsEnabled = true;
            }
            else
            {
                this.textBlockTransPostePay.Text = "";
                this.textBlockError.Text = pvdto.Code + " " + pvdto.Message;
                this.btnCancelPostePay.IsEnabled = false;
                this.btnConfirmPostePay.IsEnabled = false;
            }
            stsc.Close();
            pvdto = null;
            bv = null;
        }

        private void btnConfirmPostePay_Click(object sender, RoutedEventArgs e)
        {
            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.PaidPostePayIPPS ppdto = stsc.SendConfirmPostePay(username, password, membercode, textBlockTransPostePay.Text, "10");

            if (ppdto.Success)
            {
                this.textBlockConfirmPostePay.Text = "Ok";
            }
            else
            {
                this.textBlockConfirmPostePay.Text = "";
                this.textBlockError.Text = ppdto.Code + " " + ppdto.Message;
            }
        }

        private void btnCancelPostePay_Click(object sender, RoutedEventArgs e)
        {
            SendTransactions.SendTransactionsSoapClient stsc = new SendTransactions.SendTransactionsSoapClient();
            SendTransactions.CancelledTransactionDTO ppdto = stsc.SendCancelPostePay(username, password, membercode, textBlockTransPostePay.Text);

            if (ppdto.Success)
            {
                this.textBlockConfirmPostePay.Text = "Ok";
            }
            else
            {
                this.textBlockConfirmPostePay.Text = "";
                this.textBlockError.Text = ppdto.Code + " " + ppdto.Message;
            }
        }
    }
}