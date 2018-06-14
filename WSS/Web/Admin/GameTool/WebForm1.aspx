<%@ Page Language="C#" %>

<%@ Register Assembly="Coolite.Ext.Web" Namespace="Coolite.Ext.Web" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 
    "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Card Layout - Coolite Toolkit Examples</title>
    
    <link href="../../../../resources/css/examples.css" rel="stylesheet" type="text/css" />
    
    <script runat="server">
        protected void Next_Click(object sender, EventArgs e)
        {

            CheckButtons();
        }

        protected void Prev_Click(object sender, EventArgs e)
        {

            CheckButtons();
        }

        private void CheckButtons()
        {
            WizardLayout.Items[0].Show();

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ScriptManager ID="ScriptManager1" runat="server" />
        
        <ext:Panel ID="WizardPanel" runat="server" Title="Example Wizard" BodyStyle="padding:15px" Height="300">       
            <Body>
                <ext:CardLayout ID="WizardLayout" runat="server" ActiveItem="0">
                    <ext:Panel 
                        ID="Panel1"
                        runat="server" 
                        Html="<h1>Welcome to the Wizard!</h1><p>Step 1 of 3</p>" 
                        Border="false" 
                        Header="false" 
                        />
                    <ext:Panel 
                        ID="Panel2"
                        runat="server" 
                        Html="<h1>Card 2</h1><p>Step 2 of 3</p>" 
                        Border="false" 
                        Header="false" 
                        />
                    <ext:Panel 
                        ID="Panel3"
                        runat="server" 
                        Html="<h1>Congratulations!</h1><p>Step 3 of 3 - Complete</p>" 
                        Border="false" 
                        Header="false" 
                        />
                </ext:CardLayout> 
            </Body>         
            <Buttons>
                <ext:Button ID="btnPrev" runat="server" Text="Prev" Disabled="true" Icon="PreviousGreen">
                    <AjaxEvents>
                        <Click OnEvent="Prev_Click" ViewStateMode="Include" />
                    </AjaxEvents>
                </ext:Button>
                <ext:Button ID="btnNext" runat="server" Text="Next" Icon="NextGreen">
                    <AjaxEvents>
                        <Click OnEvent="Next_Click" ViewStateMode="Include" />
                    </AjaxEvents>
                </ext:Button>
            </Buttons>     
        </ext:Panel>
    </form>
</body>
</html>

