ALTER TABLE tb_GIS_EMEA_Ref_Business_Stream add LastUpdatedBy varchar(100),LastUpdatedDate datetime
ALTER TABLE tb_GIS_Contract_Stream_Approval_Routing add LastUpdatedBy varchar(100),LastUpdatedDate datetime
ALTER TABLE tb_GIS_Education_Stream_Approval_Routing alter column LastUpdatedBy varchar(100)
GO

CREATE procedure SP_deleteManager
(
	@managerid int,
	@internalId int
)
as
begin
	delete from tb_user_managers where user_id = @internalId and Manager_Id= @managerid
end
GO
CREATE Procedure SP_InsertManager
(
	@userid int,
	@mgrid int,
	@msg varchar(20) output
)
as
Begin
	declare @desig int
	IF NOT EXISTS (SELECT [user_id] FROM tb_user_managers WHERE [user_id]=@userid and Manager_id=@mgrid)
	Begin
		select @desig=Designation_Id from tb_user_master where internal_user_id=@mgrid
		insert into tb_user_managers(user_id,Manager_id,designation_id) values(@userid,@mgrid,@desig)
            SET @msg='0'
    End
Else
    Begin
            SET @msg = '1'
    End   
	Return @msg
End
GO

CREATE procedure SP_InsertReassignManager
(
	@managerid int,
	@reassignmanagerid int,
	@isTransfer BIT

)
as begin
	insert into tb_manager_reassign(Manager_Id,ReAssign_Manager_Id,Status) values(@managerid,@reassignmanagerid,'Assign')

	if(@isTransfer=1)
		Update tb_Request_Master Set Current_Actor = @reassignmanagerid where Current_Actor = @managerid and Workflow_id = 4
end

GO
CREATE procedure SP_DeleteReassignManager
(
	@managerid int
)
as begin
	delete from tb_manager_reassign where Manager_Id=@managerid
end

GO


CREATE Procedure SP_SearchReassignManagers
(
	@status int,
	@userid int,
	@country varchar(4)
)
as
Begin
	if @status=0
	begin
		Select 
			distinct PM.Internal_User_Id,User_Full_Name,country_id 
		from 
			tb_permissions PM,tb_User_Master UM where role_id in (2,3) 
			and PM.Internal_User_Id = UM.Internal_User_Id and UM.Country_Id IN 
			(
				SELECT 
					a.sub_region 
				FROM 
					tb_GIS_Country_Region a, tb_GIS_Country_Region b WHERE a.super_region = b.super_region
					AND b.sub_region = @country UNION SELECT @country
			) 
			AND UM.Internal_User_Id <> @userid order by User_Full_Name
	end
	else
	Begin
		Select 
			distinct PM.Internal_User_Id,User_Full_Name 
		from 
			tb_permissions PM,tb_User_Master UM where role_id in (2,3) 
			and PM.Internal_User_Id = UM.Internal_User_Id and UM.Country_Id IN 
			(
				SELECT 
					b.country_id 
				FROM 
					tb_country_master a, tb_country_master b WHERE a.region_id = b.region_id AND a.country_id = @country 
			) 
			AND UM.Internal_User_Id <> @userid order by User_Full_Name
	End	 
End
GO
CREATE procedure SP_UserRoles
(
 @id varchar(50)
)
 as 
begin
	SELECT
		 role_id,role_name 
	from 
		tb_roles 
	where 
		role_id NOT IN 
		(
			select 
				role_id
			from tb_permissions 
			where 
				internal_user_id = @id
		)
End
GO
CREATE procedure SP_SaveRoles
(
	 @id integer,
	 @role varchar(50)
)
as Begin
	 insert into tb_permissions(Internal_User_Id,Role_Id) values(@id,@role)
end
GO
USE [GLOBAL_WEBMARMS]
GO

/****** Object:  StoredProcedure [webmarms-admin].[SP_deleteRequestInfo]    Script Date: 5/25/2017 11:04:33 AM ******/
create procedure [webmarms-admin].[SP_deleteRequestInfo]
(
@id int
)
as begin
update tb_request_master set Current_Actor=0,Requester_Id=0 where Request_id=@id
end
GO

