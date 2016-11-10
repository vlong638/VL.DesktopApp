

/*==============================================================*/
/* Table: TUser                                                 */
/*==============================================================*/
if exists (select 1
            from  sysobjects
           where  id = object_id('TUser')
            and   type = 'U')
   drop table TUser
go
create table TUser (
   UserName             nvarchar(20)         not null,
   Password             nvarchar(16)         not null,
   CreateTime           datetime             not null,
   constraint PK_TUSER primary key (UserName)
)
go

/*==============================================================*/
/* Table: TUserBasicInfo                                        */
/*==============================================================*/
if exists (select 1
            from  sysobjects
           where  id = object_id('TUserBasicInfo')
            and   type = 'U')
   drop table TUserBasicInfo
go
create table TUserBasicInfo (
   UserName             nvarchar(20)         not null,
   Gender               numeric(1)           null,
   Birthday             datetime             null,
   Mobile               numeric(11)          null,
   Email                nvarchar(32)         null,
   IdCardNumber         nvarchar(18)         null,
   constraint PK_TUSERBASICINFO primary key (UserName)
)
go