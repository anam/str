<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login-admin.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml"class="bg-black">
    <head>
        <meta charset="UTF-8"/>
        <title>Log in</title>
        <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport'/>
        <!-- bootstrap 3.0.2 -->
        <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
        <!-- font Awesome -->
        <link href="css/font-awesome.min.css" rel="stylesheet" type="text/css" />
        <!-- Theme style -->
        <link href="css/AdminLTE.css" rel="stylesheet" type="text/css" />

        <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
          <script src="https://oss.maxcdn.com/libs/respond.js/1.3.0/respond.min.js"></script>
        <![endif]-->
    </head>
<body class="bg-black">
    
    <div class="form-box" id="login-box">
            <div class="header">Sign In</div>
            <form id="loginform" method="post" runat="server">
                <div class="body bg-gray">
                    <div class="form-group">
                        <asp:Label ID="lblMessage" runat="server" Text="" Visible="false"></asp:Label>
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtemailid" name="emailid" class="form-control" placeholder="Email"/>
                    </div>
                    <div class="form-group">
                        <asp:TextBox runat="server" ID="txtpassword" name="password" TextMode="Password" class="form-control" placeholder="Password"/>
                    </div>          
                </div>
                <div class="footer">                                                               
                    <button type="submit" class="btn bg-olive btn-block">Sign In</button>  
                    
                </div>
            </form>
        </div>


        <!-- jQuery 2.0.2 -->
        <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.0.2/jquery.min.js"></script>
        <!-- Bootstrap -->
        <script src="js/bootstrap.min.js" type="text/javascript"></script>        
</body>
</html>
