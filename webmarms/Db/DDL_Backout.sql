ALTER TABLE tb_GIS_EMEA_Ref_Business_Stream DROP Column LastUpdatedBy,LastUpdatedDate 
ALTER TABLE tb_GIS_Contract_Stream_Approval_Routing DROP Column LastUpdatedBy ,LastUpdatedDate 

GO


DROP procedure SP_deleteManager
DROP Procedure SP_InsertManager

DROP procedure SP_InsertReassignManager
DROP procedure SP_DeleteReassignManager
DROP Procedure SP_SearchReassignManagers

DROP PROCEDURE SP_UserRoles

DROP procedure SP_SaveRoles
DROP procedure SP_deleteRequestInfo 

