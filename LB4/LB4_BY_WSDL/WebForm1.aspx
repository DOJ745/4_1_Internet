<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LB4_BY_WSDL.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>WEB FORM</title>


    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <script>

        let url = "http://localhost:50266/Simplex.asmx";

        function CallService() {

            var objOneStr = $("#obj_one_str").val();
            var objOneInt = $("#obj_one_int").val();
            var objOneFloat = $("#obj_one_float").val();

            var objTwoStr = $("#obj_two_str").val();
            var objTwoInt = $("#obj_two_int").val();
            var objTwoFloat = $("#obj_two_float").val();

            var soapMessage = '<?xml version="1.0" encoding="utf-8"?>' +
                '<soap:Envelope xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/">' +
                '  <soap:Body>' +
                '    <Sum xmlns="FAA">' +
                '      <objOne>' +
                '        <str>' + objOneStr + '</str>' +
                '        <numberInt>' + objOneInt + '</numberInt>' +
                '        <numberFloat>' + objOneFloat + '</numberFloat>' +
                '      </objOne>' +
                '      <objTwo>' +
                '        <str>' + objTwoStr + '</str>' +
                '        <numberInt>' + objTwoInt + '</numberInt>' +
                '        <numberFloat>' + objTwoFloat + '</numberFloat>' +
                '      </objTwo>' +
                '    </Sum>' +
                '  </soap:Body>' +
                '</soap:Envelope>';

            $.ajax({
                url: url,
                type: "POST",
                dataType: "xml",
                processData: false,
                data: soapMessage,
                contentType: 'text/xml; charset="utf-8"',
                success: OnSuccess,
                error: OnError
            });

            return false;
        }

        function OnSuccess(data, status) {
            alert(data.toString());
        }

        function OnError(request, status, error) {
            alert('error');
        }

        $(document).ready(function () {
            jQuery.support.cors = true;
        });
    </script>

</head>
<body>

    <form id="form1" runat="server">

        <div>
            <h2>ADD METHOD</h2>
            <br />

            <label>Enter x:</label>
            <br />
            <asp:TextBox ID="InputX" runat="server" TextMode="Number"></asp:TextBox>
            <br />
            <label>Enter y:</label>
            <br />
            <asp:TextBox ID="InputY" runat="server" TextMode="Number"></asp:TextBox>
            <br/>
            <asp:Button ID="Button2" runat="server" Text="ADD" OnClick="addMethod" />
            <br/><br/>
            <asp:Label ID="Label2" runat="server" Text="ADD RESULT"></asp:Label>
        </div>

        <br/><br/>
        
        <div>
            <h2>CONCAT METHOD</h2>
            <br />

            <label>Enter string:</label>
            <br />
            <asp:TextBox ID="InputStr" runat="server"></asp:TextBox>
            <br />
            <label>Enter double number:</label>
            <br />
            <asp:TextBox ID="InputDouble" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button3" runat="server" Text="CONCAT" OnClick="concatMethod" />
            <br/><br/>
            <asp:Label ID="Label3" runat="server" Text="CONCAT RESULT"></asp:Label>
        </div>

        <br/><br/>

        <div>
            <h2>SUM METHOD</h2>
            <br />

            <h3>OBJ ONE</h3>
            <br/>

            <label>Enter string:</label>
            <br />
            <asp:TextBox ID="InputObjOneStr" runat="server" Text="def"></asp:TextBox>
            <br />
            <label>Enter int number:</label>
            <br />
            <asp:TextBox ID="InputObjOneInt" runat="server" TextMode="Number" Text="5"></asp:TextBox>
            <br />
            <label>Enter float number:</label>
            <br />
            <asp:TextBox ID="InputObjOneFloat" runat="server" Text="4,15"></asp:TextBox>

            <br /><br />

            <h3>OBJ TWO</h3>
            <br/>

            <label>Enter string:</label>
            <br />

            <asp:TextBox ID="InputObjTwoStr" runat="server" Text="ault"></asp:TextBox>
            <br />
            <label>Enter int number:</label>
            <br />
            <asp:TextBox ID="InputObjTwoInt" runat="server" TextMode="Number" Text="75"></asp:TextBox>
            <br />
            <label>Enter float number:</label>
            <br />
            <asp:TextBox ID="InputObjTwoFloat" runat="server" Text="27,75"></asp:TextBox>
            <br /><br />

            <asp:Button ID="Button4" runat="server" Text="SUM" OnClick="sumMethod" />
            <br/><br/>
            <asp:Label ID="Label4" runat="server" Text="SUM RESULT"></asp:Label>

        </div>

    </form>

</body>
</html>
