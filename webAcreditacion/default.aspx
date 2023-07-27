<%@ Register TagPrefix="cc2" Namespace="Sonda.Net.Validator" Assembly="SondaNetWebUI" %>
<%@ Register TagPrefix="cc1" Namespace="Sonda.Net.Control" Assembly="SondaNetWebUI" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Sonda.Net.Page.DefaultPage" debug="True" enableViewStateMac="False" %>
<!DOCTYPE HTML>
<HTML lang="es">
	<HEAD id="myHead" runat="server">
		<base target="_self">
		<META HTTP-EQUIV="Pragma" CONTENT="no-cache">
		<META HTTP-EQUIV="expires" CONTENT="0"/>
		<META HTTP-EQUIV="Cache-Control" CONTENT ="no-cache">
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
				<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
				<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
				<meta content="Javascript" name="vs_defaultClientScript" />
				<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
		<title>
			<asp:Literal id="lblpagtitle" runat="server"></asp:Literal>
		</title>
				<link href="https://fonts.googleapis.com/css?family=Oswald:300,400|Raleway:300,500,700" rel="stylesheet">
		<LINK href="Styles.css" type="text/css" rel="stylesheet"/>
		
				<script src="js/3_4_0/jquery.js" language="javascript" />
				<script src='webRecaudacion/RecaudacionNet.js' language="javascript" type="text/javascript"/>

	</HEAD>
	<body id="theBody" leftMargin="20px" topMargin="0" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" encType="multipart/form-data" runat="server" style="margin-bottom: 50px;">
			<div id="infoBar">
				<asp:label id="lblPath" runat="server"></asp:label>
				<asp:label id="lblInfo" runat="server"></asp:label>
			</div>
			<asp:placeholder id="Body" runat="server"></asp:placeholder>
			<asp:placeholder id="MyFooter" Visible="False" Runat="server"></asp:placeholder>
			<asp:placeholder id="Side" Visible="False" Runat="server"></asp:placeholder>
		</FORM>

			</body>

			</HTML>
