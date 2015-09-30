<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ConsultarDadosViaReceitaFederal.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/jquery-1.9.1.min.js"></script>
    <script src="Scripts/jquery-1.9.1.intellisense.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <div class="form-horizontal dados">

            <div class="form-group">
                <label class="control-label col-md-2">CNPJ</label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtCnpj" class="form-control" autofocus />
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-2">Informe o texto da imagem</label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="txtImgCaptcha" class="form-control" />
                    <asp:Image ImageUrl="" ID="imgCaptcha" runat="server" />
                    <asp:LinkButton Text="Buscar CNPJ" runat="server" class="btn btn-success" ID="btnBuscarCnpj" OnClick="ConsultarDados"><i class="glyphicon glyphicon-search"></i></asp:LinkButton>

                </div>
            </div>
            <div class="form-group">
                <asp:Label runat="server" ID="lblMsgRetorno" class="control-label col-md-2"></asp:Label>
            </div>
            <div class="form-group">
                <label class="control-label col-md-2">Razão Social</label>
                <div class="col-md-8">
                     <asp:TextBox runat="server" ID="txtRazaoSocial"  class="form-control " disabled/>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">Nome Fantasia</label>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtFantasia"  class="form-control " disabled/>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">CNAE</label>
                <div class="col-md-8">
                   <asp:TextBox runat="server" ID="txtCNAE"  class="form-control " disabled/>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">Endereço</label>
                <div class="col-md-8">
                     <asp:TextBox runat="server" ID="txtEndereco"   class="form-control " disabled/>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">Bairro</label>
                <div class="col-md-8">
                    <asp:TextBox runat="server" ID="txtBairro" class="form-control " disabled/>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">CEP</label>
                <div class="col-md-8">
                     <asp:TextBox runat="server" ID="txtCep"   class="form-control " disabled/>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">Cidade</label>
                <div class="col-md-8">
                     <asp:TextBox runat="server" ID="txtCidade"   class="form-control  " disabled />
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">Estado</label>
                <div class="col-md-8">
                     <asp:TextBox runat="server" ID="txtEstado"   class="form-control " disabled/>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
