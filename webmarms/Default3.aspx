<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" MasterPageFile="~/HpeNetspare.master" Inherits="Default3" %>
<%--<%@ MasterType VirtualPath="~/Default3.aspx" %>--%>
<asp:Content ID="myContent" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table align="center" style="vertical-align: top" >
                    <tr>
                        <td class="style2"  align="center">
                            <table align="center">
                                <tr>
                                    <td class="style6" colspan="7" style="text-align:center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <span class="style8 " >Welcome To NetSpares</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="200px" rowspan="10">
                                        <br />
                                        <br />
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/images/FlowChartImages/btn3rdPartyVendor.png"
                                            onmouseover="Tip('3rd Party contract data that provides back end contract information with HP')"
                                            onmouseout="UnTip()" Width="200px" />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Image ID="Image20" runat="server" ImageUrl="~/images/FlowChartImages/btn3rdPartyVendor.png"
                                            onmouseover="Tip('3rd Party contract data that provides back end contract information with HP')"
                                            onmouseout="UnTip()" Width="200px" />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Image ID="Image21" runat="server" ImageUrl="~/images/FlowChartImages/btn3rdPartyVendor.png"
                                            onmouseover="Tip('3rd Party contract data that provides back end contract information with HP')"
                                            onmouseout="UnTip()" Width="200px" />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Image ID="Image13" runat="server" Height="48px" ImageUrl="~/images/FlowChartImages/btnContractanalysisteam.png"
                                            Width="200px" />
                                    </td>
                                    <td class="style4" rowspan="10">
                                        <br />
                                        <br />
                                        <asp:Image ID="Image23" runat="server" ImageUrl="~/images/FlowChartImages/ArrowsRight2.png"
                                            Height="241px" Width="111px" />
                                    </td>
                                    <td align="left">
                                        <asp:Image ID="Image1" runat="server" Height="40px" ImageUrl="~/images/FlowChartImages/btnSAP.png"
                                            onmouseover="Tip('contract data is passed from SAP to Business warehouse')" onmouseout="UnTip()"
                                            Width="200px" />
                                    </td>
                                    <td class="style9">&nbsp;
                                    </td>
                                    <td width="100px">&nbsp;
                                    </td>
                                    <td class="style10">&nbsp;
                                    </td>
                                    <td class="style3">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top">
                                        <asp:Image ID="Image17" runat="server" ImageUrl="~/images/FlowChartImages/Arrow.png"
                                            Height="57px" Width="40px" />
                                    </td>
                                    <td class="style9">&nbsp;
                                    </td>
                                    <td width="100px">&nbsp;
                                    </td>
                                    <td class="style10">&nbsp;
                                    </td>
                                    <td class="style3">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left">
                                        <asp:Image ID="Image5" runat="server" Height="40px" onmouseover="Tip('data flows from BW  to Netspares<br/>additional data from SAP BW is fed into Netspares')"
                                            onmouseout="UnTip()" ImageUrl="~/images/FlowChartImages/btnBusinessWarehouse.png"
                                            Width="200px" />
                                    </td>
                                    <td class="style9">&nbsp;
                                    </td>
                                    <td width="100px">&nbsp;
                                    </td>
                                    <td class="style10">&nbsp;
                                    </td>
                                    <td class="style3">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" class="style15">
                                        <asp:Image ID="Image18" runat="server" ImageUrl="~/images/FlowChartImages/Arrow.png"
                                            onmouseout="UnTip()" Height="57px" Width="40px" />
                                    </td>
                                    <td class="style16"></td>
                                    <td width="100px" class="style15"></td>
                                    <td class="style17"></td>
                                    <td class="style18"></td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" class="style15">
                                        <asp:Image ID="Image6" runat="server" ImageUrl="~/images/FlowChartImages/btnNetsparesDatabase.png"
                                            onmouseover="Tip('the database where 3rd party data is synchronized with HP data.<br/>Data is used to track renewal and back end contracts and to verify service coverage for HP customers')"
                                            onmouseout="UnTip()" Width="200px" Height="40px" />
                                    </td>
                                    <td class="style16" align="left">
                                        <asp:Image ID="Image24" runat="server" ImageUrl="~/images/FlowChartImages/RightArrow.png"
                                            Height="37px" Width="86px" />
                                    </td>
                                    <td width="100px" class="style15" align="left">
                                        <asp:Image ID="Image8" runat="server" ImageUrl="~/images/FlowChartImages/btnNetsparesDatabasePortal.png"
                                            onmouseover="Tip('the portal where users can search vendor contracts<br/>and down load reports (requires access)')"
                                            onmouseout="UnTip()" Width="217px" Height="40px" />
                                    </td>
                                    <td class="style17" align="left">
                                        <asp:Image ID="Image25" runat="server" ImageUrl="~/images/FlowChartImages/RightArrow.png"
                                            Height="38px" Width="82px" />
                                    </td>
                                    <td class="style18" align="left">
                                        <asp:Image ID="Image9" runat="server" Height="40px" ImageUrl="~/images/FlowChartImages/btnUserReports.png"
                                            onmouseover="Tip('User can retrieve reports and create their own (requires access)')"
                                            onmouseout="UnTip()" Width="170px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" class="style15">
                                        <asp:Image ID="Image19" runat="server" ImageUrl="~/images/FlowChartImages/Arrow.png"
                                            Height="66px" Width="37px" />
                                    </td>
                                    <td class="style16">&nbsp;
                                    </td>
                                    <td width="100px" class="style15">&nbsp;
                                    </td>
                                    <td class="style17">&nbsp;
                                    </td>
                                    <td class="style18" rowspan="2" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Image ID="Image26" runat="server" ImageUrl="~/images/FlowChartImages/Arrows.png"
                                    Height="103px" Width="137px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" class="style15">
                                        <asp:Image ID="Image7" runat="server" ImageUrl="~/images/FlowChartImages/BtnAdminAccess.png"
                                            Height="40px" onmouseover="Tip('restricted access')" onmouseout="UnTip()" Width="200px" />
                                    </td>
                                    <td class="style16">&nbsp;
                                    </td>
                                    <td width="100px" class="style15">&nbsp;
                                    </td>
                                    <td class="style17">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" valign="top" class="style15">&nbsp;
                                    </td>
                                    <td class="style16">&nbsp;
                                    </td>
                                    <td width="100px" class="style15">&nbsp;
                                    </td>
                                    <td class="style17">&nbsp;
                                    </td>
                                    <td class="style18" rowspan="3" valign="top" align="left">
                                        <asp:Image ID="Image10" runat="server" ImageUrl="~/images/FlowChartImages/btnUser.png"
                                            Width="85px" />
                                        &nbsp;&nbsp;
                                <asp:Image ID="Image22" runat="server" ImageUrl="~/images/FlowChartImages/btnUser.png"
                                    Width="85px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" align="left" rowspan="2">
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <br />
                                    </td>
                                    <td class="style9" align="left">&nbsp;
                                    </td>
                                    <td width="100px">&nbsp;
                                    </td>
                                    <td class="style10">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style12"></td>
                                    <td width="100px" class="style11"></td>
                                    <td class="style13"></td>
                                </tr>
                            </table>
                            <br />
                        </td>
                    </tr>
                </table>

 </asp:Content>