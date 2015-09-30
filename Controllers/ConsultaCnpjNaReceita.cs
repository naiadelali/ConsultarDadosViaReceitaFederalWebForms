using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsultarDadosViaReceitaFederal.Controllers
{
    public class ConsultaCnpjNaReceita
    {
        private String _erro;
        public CookieContainer _cookies = new CookieContainer();
        private String UrlDominio = "http://www.receita.fazenda.gov.br";
        private String urlBaseReceitaFederal = "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/";
        private String paginaPrincipal = "cnpjreva_solicitacao2.asp";
        private String paginaCaptcha = "captcha/gerarCaptcha.asp";
        private String paginaValidacao = "Valida.asp";
        private String captcha = "";
        public String ErroDetectado { get { return _erro; } }

        #region Recupera a Imagem do Captcha
        /// <summary>
        /// Chamada inicial da classe, resposável por popular a imagem e criar os cookies em memória
        /// para serem confrontados no ato do post de validação
        /// </summary>
        /// <returns></returns>
        public string RecuperaCaptcha()
        {
            try
            {
                var htmlResult = string.Empty;

                using (var wc = new Infra.CookieAwareWebClient(_cookies))
                {
                    wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                    wc.Headers[HttpRequestHeader.KeepAlive] = "300";
                    htmlResult = wc.DownloadString(urlBaseReceitaFederal + paginaPrincipal);
                }

                if (htmlResult.Length > 0)
                {
                    var wc2 = new Infra.CookieAwareWebClient(_cookies);
                    wc2.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                    wc2.Headers[HttpRequestHeader.KeepAlive] = "300";
                    byte[] data = wc2.DownloadData(urlBaseReceitaFederal + paginaCaptcha);

                    //converter imagem byte em base64
                    captcha = "data:image/jpeg;base64," + Convert.ToBase64String(data, 0, data.Length);
                }
                else
                    throw new Exception("Não foi possível gerar o Captcha");
            }
            catch (Exception ex)
            {
                _erro = ex.Message;
            }
            try
            {
                if (!(captcha.Length > 0))
                    throw new Exception("Não foi possível carregar o a imagem de segurança");
            }
            catch (Exception ex)
            {
                _erro = ex.Message;
                return null;
            }

            return captcha;

        }
        #endregion

        #region Consulta os Dados na Sefaz
        /// <summary>
        /// Consulta a Secretaria da Fazenda os Dados enviados para retornar o Cartão com os Dados do CNPJ
        /// </summary>
        /// <param name="cnpj"></param>
        /// <param name="captcha"></param>

        public string ObterDados(string aCNPJ, string aCaptcha, CookieContainer cookies)
        {
            _cookies = cookies;
            var request = (HttpWebRequest)WebRequest.Create(urlBaseReceitaFederal + paginaValidacao);
            request.ProtocolVersion = HttpVersion.Version10;
            request.CookieContainer = _cookies;
            request.Method = "POST";

            var postData = string.Empty;
            postData += "origem=comprovante&";
            postData += "cnpj=" + new Regex(@"[^\d]").Replace(aCNPJ, string.Empty) + "&";
            postData += "txtTexto_captcha_serpro_gov_br=" + aCaptcha + "&";
            postData += "submit1=Consultar&";
            postData += "search_type=cnpj";

            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var stHtml = new StreamReader(request.GetResponse().GetResponseStream(), Encoding.GetEncoding("ISO-8859-1"));

            return stHtml.ReadToEnd();
        }
        #endregion
    }
}