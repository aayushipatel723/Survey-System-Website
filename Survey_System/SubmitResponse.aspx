<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubmitResponse.aspx.cs" Inherits="Survey_System.SubmitResponse" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Submit Response</title>
    <link href="SubmitResponse.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Submit Response</h2>
            <asp:Repeater ID="QuestionsRepeater" runat="server" OnItemDataBound="QuestionsRepeater_ItemDataBound">
                <ItemTemplate>
                    <div>
                        <h3><%# Eval("QuestionText") %></h3>
                        <asp:HiddenField ID="QuestionID" runat="server" Value='<%# Eval("QuestionID") %>' />

                        <!-- For TextBox Field -->
                        <asp:TextBox ID="ResponseTextBox" runat="server" Visible="false" />

                        <!-- For RadioButton Options -->
                        <asp:Repeater ID="OptionsRepeater" runat="server" Visible="false">
                            <ItemTemplate>
                                <div>
                                    <asp:RadioButton ID="OptionRadioButton" runat="server" GroupName='<%# Eval("QuestionID") %>' Text='<%# Eval("OptionText") %>' />
                                    <asp:HiddenField ID="OptionID" runat="server" Value='<%# Eval("OptionID") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <!-- For CheckBox Options -->
                        <asp:Repeater ID="CheckBoxOptionsRepeater" runat="server" Visible="false">
                            <ItemTemplate>
                                <div>
                                    <asp:CheckBox ID="OptionCheckBox" runat="server" Text='<%# Eval("OptionText") %>' />
                                    <asp:HiddenField ID="OptionID" runat="server" Value='<%# Eval("OptionID") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <!-- Submit button -->
            <asp:Button ID="SubmitResponseButton" runat="server" Text="Submit Response" OnClick="SubmitResponseButton_Click" />
        </div>
    </form>
</body>
</html>