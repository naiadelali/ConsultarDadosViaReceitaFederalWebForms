using ConsultarDadosViaReceitaFederal.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ConsultarDadosViaReceitaFederal.Models;

namespace ConsultarDadosViaReceitaFederal
{
    public partial class Default : System.Web.UI.Page
    {
        private ConsultaCnpjNaReceita _consultaCnpjNaReceita = new ConsultaCnpjNaReceita();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaImagemCaptcha();
                
            }
        }

        private void CarregaImagemCaptcha()
        {
            try
            {
                imgCaptcha.ImageUrl = _consultaCnpjNaReceita.RecuperaCaptcha();
                ViewState["cookies"] = _consultaCnpjNaReceita._cookies;
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }

        protected void ConsultarDados(object sender, EventArgs e)
        {
            try
            {
                
                if (!ValidaCampos())
                {
                    var cnpj = txtCnpj.Text
                                      .Trim()
                                      .Replace(".","")
                                      .Replace("-", "")
                                      .Replace("/", "");

                    var captcha = txtImgCaptcha.Text
                                               .Trim();

                    var msg = string.Empty;

                    var resp = _consultaCnpjNaReceita.ObterDados(cnpj, 
                                                                 captcha,
                                                                 (CookieContainer)ViewState["cookies"]);
                    var empresa = resp.Length > 0 ? Infra.FormatarDados.MontarObjEmpresa(cnpj, resp) : null;

                    if (resp.Contains("Verifique se o mesmo foi digitado corretamente"))
                        msg = "O número do CNPJ não foi digitado corretamente";

                    if (resp.Contains("Erro na Consulta"))
                        msg += "Os caracteres não conferem com a imagem";

                    try
                    {
                        if (msg.Length > 0)
                            throw new Exception(msg);
                    }
                    catch (Exception ex)
                    {

                        lblMsgRetorno.Text = ex.Message;
                    }

                    PreencherCamposNaTelaDa(empresa);
                }

               

            }
            catch (Exception ex)
            {
                lblMsgRetorno.Text = ex.Message;
            }
            if (_consultaCnpjNaReceita.ErroDetectado != null)
            {
                lblMsgRetorno.Text = _consultaCnpjNaReceita.ErroDetectado;

            }
            else
            {
              
            }
        }

        private void PreencherCamposNaTelaDa(Empresa empresa)
        {
            txtBairro.Text = empresa.Bairro;
            txtCep.Text = empresa.Cep;
            txtCidade.Text = empresa.Cidade;
            txtCNAE.Text = empresa.Cnae;
            txtCnpj.Text = empresa.CNPJ;
            txtEndereco.Text = empresa.Endereco;
            txtEstado.Text = empresa.Estado;
            txtFantasia.Text = empresa.NomeFantasia;
            txtRazaoSocial.Text = empresa.Razaosocial;
        }

        private bool ValidaCampos()
        {
            bool flagTemErro = false;
            string mensagemErro = string.Empty;

            mensagemErro = "Por gentileza, verifique os itens abaixo:" + Environment.NewLine;
            if (string.IsNullOrEmpty(txtCnpj.Text))
            {
                flagTemErro = true;
                mensagemErro = "- O Campo CNPJ é obrigatório"+ Environment.NewLine;
            }
            if (string.IsNullOrEmpty(txtImgCaptcha.Text))
            {
                flagTemErro = true;
                mensagemErro = "- O Campo texto da imagem é obrigatório" + Environment.NewLine;
            }

            if (flagTemErro)
            {
                lblMsgRetorno.Text = mensagemErro;
            }
            return flagTemErro;
        }
    }
}