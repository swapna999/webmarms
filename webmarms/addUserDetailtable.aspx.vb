Imports System.Data.SqlClient

Partial Class Admin_addUserDetailtable
    Inherits System.Web.UI.Page
    Public conn As New SqlConnection()



    Public sqlStr, table_auth, var_regionId, var_AdminUser, var_UserId As String
    Protected strCountry, strDomain, strDesignation, strDomainId, strAuthenticated As String
    Protected strRole As String
    Protected strManager As String
    'Global WebMarms 17.06 June Release
    Public mgrId As Integer
    Public mgrDesignation, SupermgrId As String



#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

        Dim ID As Integer

        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        If Not Page.IsPostBack Then
            DisplayData(ID)          
            'lblIsActive = CType(e.Item.FindControl("Label1"), Label).Text
            DisplayRoles(ID)
            DisplayManagers(ID)
        End If
    End Sub
    Private Sub DisplayData(ByVal id As Integer)
        'Dim strTempDomainId As String = getDomainId()

        sqlStr = "SELECT Internal_User_Id, ltrim(rtrim(User_Full_Name)) as [Name], c.Designation_Name as [Designation], "
        sqlStr = sqlStr + " (select distinct Country_Name from tb_Country_Master where Country_Id=d.Country_Id) as [Country],"
        sqlStr = sqlStr + "(select distinct domain_name from tb_domain_names where Domain_Id=d.Domain_Id) as [Domain],"
        sqlStr = sqlStr + "d.user_ID as [UserID], d.user_email as [Email], "
        sqlStr = sqlStr + " Case when d.User_Authenticated= 'Y' then 'Active'  else 'In Active' end as [Active User],Case when d.International_User = 'Y' then 'YES' else 'NO' end  as [International User] "
        sqlStr = sqlStr + " FROM tb_designations c inner join tb_user_master d on d.Designation_Id = c.Internal_Designation_Id"
        sqlStr = sqlStr + " And Internal_User_Id = '" & id & "'"


        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim myDS As DataSet = New DataSet()
        Dim myDA As SqlDataAdapter = New SqlDataAdapter(sqlStr, conn)
        myDA.Fill(myDS)
        rpt.DataSource = myDS
        rpt.DataBind()
        myDA.Dispose()
        conn.Close()

    End Sub

    Private Sub DisplayManagers(ByVal id As Integer)
        'Dim strTempDomainId As String = getDomainId()

        sqlStr = " SELECT DISTINCT a.internal_user_id AS userID,b.User_Manager_Id, LTRIM(RTRIM(a.user_full_name)) AS [Name], "
        sqlStr = sqlStr + " (SELECT internal_user_id AS manager_ID from tb_user_master where internal_user_id = b.manager_id) as Manager_Id, "
        sqlStr = sqlStr + " (SELECT LTRIM(RTRIM(user_full_name)) AS [Manager] from tb_user_master where internal_user_id = b.manager_id) as [Manager] "
        sqlStr = sqlStr + " FROM tb_user_master a inner join tb_user_managers b on"
        sqlStr = sqlStr + " a.internal_user_id = b.user_id "
        'Commente by rajesh-06-07-2012
        'AND a.country_id = c.country_id "'Commente by rajesh-06-07-2012-end here

        'If Session("region_id") = 2 Then
        '    sqlStr = sqlStr + " AND c.region_id=2 "
        'ElseIf Session("region_id") = 1 Then
        '    'sqlStr = sqlStr + " AND c.Country_id IN (SELECT DISTINCT sub_region FROM tb_GIS_Country_Region WHERE super_region = (SELECT DISTINCT super_region FROM tb_GIS_Country_Region WHERE sub_region = '" & Session("country_id") & "')) "
        '    sqlStr = sqlStr + " AND c.region_id=1 "
        'ElseIf Session("region_id") = 3 Then
        '    sqlStr = sqlStr + " AND c.region_id=3 "
        'End If


        'Response.Write(sqlStr)
        'Response.End()
        sqlStr = sqlStr + " AND b.user_id = '" & id & "'"
        sqlStr = sqlStr + lblOrderBy.Text
        'Response.Write(sqlStr)
        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim myDS As DataSet = New DataSet()
        Dim myDA As SqlDataAdapter = New SqlDataAdapter(sqlStr, conn)
        myDA.Fill(myDS)
        If (Not myDS Is Nothing) Then
            If (myDS.Tables.Count > 0) Then
                If (myDS.Tables(0).Rows.Count > 0) Then
                    DataGrid1.DataSource = myDS
                    DataGrid1.DataBind()
                End If
            End If
        End If
        If (DataGrid1.Items.Count > 0) Then
            DataGrid1.Visible = True
        Else
            DataGrid1.Visible = False
            Lbl_Error_Message.Text = "No Records Found"
        End If
        myDA.Dispose()
        conn.Close()

        If DataGrid1.PageCount > 10 Then
            lblPages.Visible = True
            lblPages.Text = "Total Pages : " & DataGrid1.PageCount
        Else
            lblPages.Visible = False
            lblPages.Text = ""
        End If

    End Sub
    Sub DataGrid1_PageChanger(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged

        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        DisplayManagers(ID)
    End Sub

    Sub PageIndexChanged_Click(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        DataGrid1.CurrentPageIndex = e.NewPageIndex
        DisplayManagers(ID)
    End Sub

    Sub SortCommand_Click(ByVal sender As Object, ByVal e As DataGridSortCommandEventArgs)
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        Try
            If hdnSortCol.Text = e.SortExpression Then
                If hdnSort.Text = "ASC" Then
                    hdnSort.Text = "DESC"
                Else
                    hdnSort.Text = "ASC"
                End If
            Else
                hdnSortCol.Text = e.SortExpression
                hdnSort.Text = "ASC"
            End If

            lblOrderBy.Text = " ORDER BY " & e.SortExpression & " " & hdnSort.Text

            DataGrid1.CurrentPageIndex = 0
            DisplayManagers(ID)
            Lbl_Error_Message.Visible = False
        Catch ex As Exception
            Lbl_Error_Message.Text = ex.Message
            Lbl_Error_Message.Visible = True
        End Try
    End Sub
    Sub Check_User_Auth()

        Dim sqlStr As String

        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        sqlStr = "Select Region_Id,Adminuser from tb_Country_Master ctry,tb_User_Master usr where ctry.country_id = usr.Country_Id and usr.user_Id='" & var_UserId & "'"
        Dim cmd As New SqlCommand()
        cmd.Connection = conn
        cmd.CommandType = CommandType.Text
        cmd.CommandText = sqlStr
        Dim Dr_Region As SqlDataReader
        Try

            Dr_Region = cmd.ExecuteReader
            While Dr_Region.Read
                var_regionId = Dr_Region.Item("Region_Id").ToString
                var_AdminUser = Dr_Region.Item("AdminUser").ToString
                Lbl_Error_Message.Visible = False
            End While

        Catch ex As Exception
            Lbl_Error_Message.Text = ex.Message
            Lbl_Error_Message.Visible = True
        End Try

        Dr_Region.Close()
        conn.Close()
    End Sub
    Public Function checkManagerAlreadyExists(ByVal strNewManagerId As String, ByVal intIndex As Integer) As Boolean
        Dim strOldManagerIds As String
        For intcount As Integer = 0 To DataGrid1.Items.Count - 1
            If (intIndex <> intcount) Then
                strOldManagerIds = CType(DataGrid1.Items(intcount).FindControl("lblManagerIds"), Label).Text
                If (strOldManagerIds = strNewManagerId) Then
                    Return True
                Else
                    Return False
                End If
            End If
        Next
    End Function

    Public Sub DataGrid1_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles DataGrid1.UpdateCommand
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        Dim cmdCustomers As SqlCommand
        Dim blnIsRepeated As Boolean
        Dim strInternalID As String = CType(e.Item.FindControl("lblInternalID"), Label).Text
        Dim strOldManagerID As Int16 = CType(e.Item.FindControl("lblManagerID"), Label).Text

        Dim strManagerID As Int16 = CType(e.Item.FindControl("ddlManager"), DropDownList).SelectedItem.Value
        Dim intIndex As Integer = e.Item.ItemIndex

        Dim strSql As String

        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        If strInternalID = "" Then
            conn.Close()
            Exit Sub
        Else
            blnIsRepeated = checkManagerAlreadyExists(strManagerID.ToString, intIndex)
            If (blnIsRepeated) Then
                strSql = "declare @OldID as int; "
                strSql = strSql + " declare @newID as int; "
                strSql = strSql + " select @OldID=user_manager_id from tb_User_Managers where User_Id='" & strInternalID & "' and Manager_Id='" & strOldManagerID & "';"
                strSql = strSql + " select @newID=user_manager_id from tb_User_Managers where User_Id='" & strInternalID & "' and Manager_Id='" & strManagerID & "';"
                'strSql = "UPDATE tb_user_managers SET Manager_Id = " & strManagerID & " WHERE User_Id='" & strInternalID & "' AND Manager_Id = '" & strOldManagerID & "'; "
                strSql = strSql + " UPDATE tb_user_managers SET Manager_Id = " & strManagerID & " WHERE user_manager_id=@OldID;"
                strSql = strSql + " UPDATE tb_user_managers SET Manager_Id = " & strOldManagerID & " WHERE user_manager_id=@newID;"
                If strOldManagerID <> strManagerID Then
                    strSql = strSql + " INSERT INTO tb_Audit VALUES('tb_user_managers', 'Manager_Id', " & strInternalID & ", '" & strOldManagerID & "', '" & strManagerID & "', '" & Session("user_id") & "', getdate()); "
                End If

                cmdCustomers = New SqlCommand(strSql, conn)
                cmdCustomers.ExecuteReader()
                DataGrid1.EditItemIndex = -1
            Else
                If strOldManagerID <> strManagerID Then
                    strSql = "insert into tb_User_Managers(User_Id,Manager_Id,Designation_Id) select distinct '" & strInternalID & "'," & strManagerID & ",Designation_Id from tb_user_master where Internal_User_Id=" & strManagerID & "; "
                Else
                    strSql = "UPDATE tb_user_managers SET Manager_Id = " & strManagerID & " WHERE User_Id='" & strInternalID & "' AND Manager_Id = '" & strOldManagerID & "'; "
                End If

                If strOldManagerID <> strManagerID Then
                    strSql = strSql + " INSERT INTO tb_Audit VALUES('tb_user_managers', 'Manager_Id', " & strInternalID & ", '" & strOldManagerID & "', '" & strManagerID & "', '" & Session("user_id") & "', getdate()); "
                End If

                cmdCustomers = New SqlCommand(strSql, conn)
                cmdCustomers.ExecuteReader()
                DataGrid1.EditItemIndex = -1
            End If
            conn.Close()

            DisplayManagers(ID)
        End If
    End Sub
    Sub DataGrid1_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        strManager = CType(E.Item.FindControl("lblManager"), Label).Text

        DataGrid1.EditItemIndex = CInt(E.Item.ItemIndex)
        DisplayManagers(ID)
    End Sub

    Public Sub DataGrid1_Cancel(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        DataGrid1.EditItemIndex = -1
        DisplayManagers(ID)
    End Sub

    Public Sub SetManagerIndex(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ed As System.Web.UI.WebControls.DropDownList
        ed = sender
        ed.SelectedIndex = ed.Items.IndexOf(ed.Items.FindByText(strManager))
    End Sub

    Public Function BindTheManager()
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Dim sqlMgr As String

        sqlMgr = " SELECT DISTINCT a.internal_user_id AS ManagerID, LTRIM(RTRIM(a.user_full_name)) AS ManagerName "
        sqlMgr = sqlMgr + " FROM tb_user_master a, tb_permissions b, tb_country_master c "
        sqlMgr = sqlMgr + " WHERE a.internal_user_id = b.internal_user_id AND b.role_id > 1 "
        'commented by rajesh-06-07-2012
        'AND a.country_id = c.country_id "
        'commented by rajesh-06-07-2012-end

        If Session("region_id") = 2 Then
            sqlMgr = sqlMgr + " AND c.region_id=2 "
        ElseIf Session("region_id") = 1 Then
            sqlMgr = sqlMgr + " AND c.Country_id IN (SELECT DISTINCT sub_region FROM tb_GIS_Country_Region WHERE super_region = (SELECT DISTINCT super_region FROM tb_GIS_Country_Region WHERE sub_region = '" & Session("country_id") & "')) "
        End If

        sqlMgr = sqlMgr + " ORDER BY ManagerName"

        Dim cmdDesig As SqlCommand = New SqlCommand(sqlMgr, conn)

        cmdDesig.CommandType = CommandType.Text

        Return cmdDesig.ExecuteReader(CommandBehavior.CloseConnection)
    End Function


    'Public Sub SearchUserInfo(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSearch.TextChanged
    '    Dim ID As Integer
    '    ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
    '    DisplayManagers(ID)

    'End Sub

    Protected Sub lbtnAddManager_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbtnAddManager.Click
        'grdUserInfo.Visible = False
        'lblPages.Visible = False
        ddlManagers.Enabled = False
        lblManager.Enabled = False
        btnSubmit.Enabled = False
        lblAddManagerMessage.Visible = False
        lblemail.Visible = True
        txtManagerEmail.Visible = True
        btnCancel.Visible = True
        btnSearch.Visible = True
        Panel3.Visible = True
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        DataGrid1.Visible = True
        lblPages.Visible = True
        ddlManagers.Enabled = False
        lblManager.Enabled = False
        btnSubmit.Enabled = False
        lblAddManagerMessage.Visible = False
        Panel3.Visible = False
        txtManagerEmail.Text = String.Empty
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        DisplayManagers(ID)

        Dim status As String
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        'Using con As New SqlConnection(conn)

        Using cmd As New SqlCommand("SP_InsertManager", conn)

            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@email", txtManagerEmail.Text)
            cmd.Parameters.AddWithValue("@mgrid", ddlManagers.SelectedValue.ToString())
            'cmd.Parameters.AddWithValue("@desig", ViewState("mgrdesignation"))

            cmd.Parameters.Add("@msg", SqlDbType.VarChar, 30)

            cmd.Parameters("@msg").Direction = ParameterDirection.Output

            cmd.ExecuteNonQuery()

            conn.Close()

            status = cmd.Parameters("@msg").Value.ToString()

        End Using
        If status = 0 Then
            lblAddManagerMessage.Visible = True
            lblAddManagerMessage.Text = "Record Submitted"
            Panel3.Visible = False
            txtManagerEmail.Text = String.Empty
            DataGrid1.Visible = True
            lblPages.Visible = True
            'txtSearch.Text = String.Empty
            DisplayManagers(ID)
            lblAddManagerMessage.Visible = False
        Else
            lblAddManagerMessage.Visible = True
            lblAddManagerMessage.Text = "Manager is already available"
        End If
    End Sub

    Protected Sub DataGrid1_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.DeleteCommand
        Dim id As Integer
        id = Int32.Parse(e.CommandArgument.ToString())
        Deleterecord("delete from tb_user_managers where User_Manager_Id='" & id & "'")
        DisplayManagers(id)
    End Sub
    Public Sub Deleterecord(ByVal query As String)
        Dim cmd As SqlCommand

        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        cmd = New SqlCommand(query, conn)
        cmd.ExecuteNonQuery()
        conn.Close()
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        If txtManagerEmail.Text <> String.Empty Then
            ddlManagers.Items.Clear()
            LoadDropDownList()
        End If

    End Sub

    Protected Sub LoadDropDownList()
        Dim status As String

        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim myDS As DataSet = New DataSet()
        Dim cmd As SqlCommand = New SqlCommand("SP_SearchManagers", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@email", txtManagerEmail.Text)
        cmd.Parameters.Add("@msg", SqlDbType.VarChar, 30)
        cmd.Parameters("@msg").Direction = ParameterDirection.Output
        Dim myDA As SqlDataAdapter = New SqlDataAdapter(cmd)

        myDA.Fill(myDS)
        If (Not myDS Is Nothing) Then
            If (myDS.Tables.Count > 0) Then
                If (myDS.Tables(0).Rows.Count > 0) Then
                    ddlManagers.DataSource = myDS.Tables(0)

                    ddlManagers.DataTextField = "Name"

                    ddlManagers.DataValueField = "userID"

                    ddlManagers.DataBind()
                End If
            End If

        End If
        'If (ddlManagers.Items.Count > 0) Then
        '    ddlManagers.Visible = True
        '    ddlManagers.SelectedItem.Enabled = False

        'Else
        '    ddlManagers.Visible = False
        'End If
        myDA.Dispose()
        conn.Close()
        status = cmd.Parameters("@msg").Value.ToString()
        If status = 1 Then
            ddlManagers.Enabled = True
            lblManager.Visible = True
            btnSubmit.Enabled = True
            lblAddManagerMessage.Enabled = False
        Else
            ddlManagers.Items.Clear()
            ddlManagers.Enabled = False
            btnSubmit.Enabled = False
        End If

    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub


    Protected Sub btnSearchEmail_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        ''Dim txtname As TextBox = grdUserInfo.FindControl("txtUname")
        'Dim txt As TextBox = DirectCast(grdUserInfo.FindControl("txtUname"), TextBox)
        ''Dim txtname As TextBox = grdUserInfo.FindControl("txtUname")
        ''            Dim txtValue As String = CType(grdUserInfo.Items(0).FindControl("txtUname"), TextBox).Text
        'Dim txtValue As String = CType(grdUserInfo.Items(0).FindControl("txtUname"), TextBox).Text
        'Dim data As String = grdUserInfo.SelectedRows(0).Cells(5).Value.ToString
        'Dim myText As String
        'For Each dataGridItem As DataGridItem In grdUserInfo.Items
        '    If ataGridItem.FindControl("txtUname")=Nullable then
        '    myText = CType(dataGridItem.FindControl("txtUname"), TextBox).Text
        '    'myText = DirectCast(dataGridItem.FindControl("txtQuantity"), TextBox).Text
        'Next
        'Dim myText As String
        'For Each dataGridItem As DataGridItem In grdUserInfo.Items
        '    myText = CType(dataGridItem.FindControl("lblManager"), Label).Text
        '    'myText = DirectCast(dataGridItem.FindControl("txtQuantity"), TextBox).Text
        'Next


    End Sub


    Private Sub DisplayRoles(ByVal id As Integer)
        sqlStr = " SELECT c.Internal_user_id,c.role_id, LTRIM(RTRIM(User_Full_Name)) AS [Name], b.role_name as [Role] "
        sqlStr = sqlStr + " FROM tb_user_master a inner join tb_permissions c  on c.internal_user_id = a.internal_user_id"
        sqlStr = sqlStr + " inner join tb_roles b on  c.role_id = b.role_id "
        sqlStr = sqlStr + " AND c.internal_user_id = '" & id & "' "
        'conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim myDS As DataSet = New DataSet()
        Dim myDA As SqlDataAdapter = New SqlDataAdapter(sqlStr, conn)

        myDA.Fill(myDS)
        grdUserInfo.DataSource = myDS
        grdUserInfo.DataBind()
        myDA.Dispose()
        conn.Close()
    End Sub

    

    Public Sub grdUserInfo_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())

        Dim cmdCustomers As SqlCommand

        Dim strInternalID As String = CType(e.Item.FindControl("lblInternalID"), Label).Text
        Dim strOldRoleID As Int16 = CType(e.Item.FindControl("lblRoleID"), Label).Text

        Dim strRoleID As Int16 = CType(e.Item.FindControl("ddlRole"), DropDownList).SelectedItem.Value


        Dim strSql As String

        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        If strInternalID = "" Then
            conn.Close()
            Exit Sub
        Else
            'strSql = "UPDATE tb_permissions SET role_Id = " & strRoleID & " WHERE Internal_User_Id='" & strInternalID & "' AND role_id = '" & strOldRoleID & "'; "
            strSql = "select 1 from tb_permissions where Internal_User_Id=" & strInternalID & " and Role_Id=" & strRoleID & ""
            cmdCustomers = New SqlCommand(strSql, conn)
            Dim drRoleExist As SqlDataReader = cmdCustomers.ExecuteReader()
            If drRoleExist.HasRows Then
                Lbl_Error_Message.Text = "The role you choose for the user already assinged to the user."
                Lbl_Error_Message.Visible = True
            Else
                Lbl_Error_Message.Visible = False
                'strSql = "insert into tb_permissions values (" & strInternalID & "," & strRoleID & ")"
                strSql = "update tb_permissions set Role_Id = " & strRoleID & " where internal_user_id = " & strInternalID & ""

                If strOldRoleID <> strRoleID Then
                    strSql = strSql + " INSERT INTO tb_Audit VALUES('tb_permissions', 'role_Id', " & strInternalID & ", '" & strOldRoleID & "', '" & strRoleID & "', '" & Session("user_id") & "', getdate()); "
                End If
                drRoleExist.Close()
                cmdCustomers = New SqlCommand(strSql, conn)
                cmdCustomers.ExecuteReader()
                grdUserInfo.EditItemIndex = -1
            End If
            conn.Close()
            LoadDropdown()
            DisplayRoles(ID)
        End If
    End Sub

    Sub grdUserInfo_ItemCommand(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)

        Dim cmdCustomers As SqlCommand
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        Dim strSql As String
        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If


        Select Case (CType(e.CommandSource, LinkButton)).CommandName

            Case "Delete"
                Dim strInternalID As String = CType(e.Item.FindControl("lblTempInternalID"), Label).Text
                Dim strOldRoleID As Int16 = CType(e.Item.FindControl("lblTempRoleID"), Label).Text
                strSql = " Select count(role_id) from tb_permissions where internal_user_id=" & strInternalID & ""
                cmdCustomers = New SqlCommand(strSql, conn)
                Dim drRecords As SqlDataReader = cmdCustomers.ExecuteReader()
                Dim intCount As Integer = 1
                If drRecords.HasRows Then
                    While drRecords.Read
                        intCount = CType(drRecords(0), Integer)
                    End While

                End If
                drRecords.Close()
                If (intCount > 1) Then
                    strSql = "delete from tb_permissions where internal_user_id=" & strInternalID & " and  role_id=" & strOldRoleID & ""
                    strSql = strSql + " INSERT INTO tb_Audit VALUES('tb_permissions', 'role_Id', " & strInternalID & ", '" & strOldRoleID & "', '" & strOldRoleID & "', '" & Session("user_id") & "', getdate()); "
                    cmdCustomers = New SqlCommand(strSql, conn)
                    cmdCustomers.ExecuteReader()
                Else
                    Lbl_Error_Message.Text = "Atleast one role should be assigned to user"
                    Lbl_Error_Message.Visible = True
                End If
            Case Else
        End Select
        conn.Close()

        DisplayRoles(ID)

    End Sub
    Sub grdUserInfo_Edit(ByVal Sender As Object, ByVal E As DataGridCommandEventArgs)
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        strRole = CType(E.Item.FindControl("lblRole"), Label).Text

        grdUserInfo.EditItemIndex = CInt(E.Item.ItemIndex)
        DisplayRoles(ID)
    End Sub

    Public Sub grdUserInfo_Cancel(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())
        grdUserInfo.EditItemIndex = -1
        DisplayRoles(ID)
    End Sub
    Public Sub SetRoleIndex(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim ed As System.Web.UI.WebControls.DropDownList
        ed = sender
        ed.SelectedIndex = ed.Items.IndexOf(ed.Items.FindByText(strRole))
    End Sub

    Public Function BindTheRole()
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim cmdDesig As SqlCommand = New SqlCommand("SELECT role_id AS roleID, role_name AS roleName FROM tb_roles", conn)

        cmdDesig.CommandType = CommandType.Text

        Return cmdDesig.ExecuteReader(CommandBehavior.CloseConnection)
    End Function


    Protected Sub Addroles_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Addroles.Click
        Panel1.Visible = True
        LoadDropdown()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RolesDropdown.SelectedIndexChanged

    End Sub

   
    Protected Sub LoadDropdown()
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())

        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim myDS As DataSet = New DataSet()
        Dim cmd As SqlCommand = New SqlCommand("SP_UserRoles", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", ID)
        Dim myDA As SqlDataAdapter = New SqlDataAdapter(cmd)

        myDA.Fill(myDS)
        If (Not myDS Is Nothing) Then
            If (myDS.Tables.Count > 0) Then
                If (myDS.Tables(0).Rows.Count > 0) Then
                    RolesDropdown.DataSource = myDS.Tables(0)

                    RolesDropdown.DataTextField = "role_name"

                    RolesDropdown.DataValueField = "role_id"

                    RolesDropdown.DataBind()
                Else
                    RolesDropdown.Items.Clear()
                End If
            End If
        End If
        If (RolesDropdown.Items.Count > 0) Then
            RolesDropdown.Visible = True
            'RolesDropdown.SelectedItem.Enabled = False

        Else
            RolesDropdown.Visible = False
        End If
        myDA.Dispose()
        conn.Close()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        conn.ConnectionString = ConfigurationSettings.AppSettings("Connection String")
        Dim ID As Integer
        ID = Convert.ToInt32(Request.QueryString("Internal_User_Id").ToString())

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If

        Dim cmd As SqlCommand = New SqlCommand("SP_SaveRoles", conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@id", ID)
        cmd.Parameters.AddWithValue("@role", RolesDropdown.SelectedValue.ToString())
        cmd.ExecuteNonQuery()
        conn.Close()
        DisplayRoles(ID)
        LoadDropdown()
    End Sub
    Public Sub GetRowColor(ByVal sender As Object)

        Dim color As String = "white"

        If Not String.IsNullOrEmpty(sender.ToString()) Then
            Dim status As String = sender.ToString()

            Select (status)

                Case "Active"
                    color = "green"

                Case "In Active"
                    color = "red"
            End Select

        End If

    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Panel1.Visible = False
    End Sub

    Protected Sub grdUserInfo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdUserInfo.SelectedIndexChanged

    End Sub

    Protected Sub txtManagerEmail_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtManagerEmail.TextChanged

    End Sub
End Class
