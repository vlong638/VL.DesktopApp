Use [VL.Blog];
/*==============================================================*/
/* Table: TBlog                                                 */
/*==============================================================*/
if exists (select 1
            from  sysobjects
           where  id = object_id('TBlog')
            and   type = 'U')
   drop table TBlog
go
create table TBlog (
   UserName             nvarchar(20)         not null,
   BlogId               uniqueidentifier     not null,
   Title                nvarchar(50)         not null,
   BreviaryContent      nvarchar(100)        not null,
   CreatedTime          datetime             not null,
   LastEditTime         datetime             not null,
   IsVisible            numeric(1)           not null default 1,
   constraint PK_TBLOG primary key (BlogId)
)
go
/*==============================================================*/
/* Table: TBlogDetail                                           */
/*==============================================================*/
if exists (select 1
            from  sysobjects
           where  id = object_id('TBlogDetail')
            and   type = 'U')
   drop table TBlogDetail
go
create table TBlogDetail (
   BlogId               uniqueidentifier     not null,
   Content              nvarchar(max)        not null,
   constraint PK_TBLOGDETAIL primary key (BlogId)
)
go
/*==============================================================*/
/* Table: TBlogTagMapper                                        */
/*==============================================================*/
if exists (select 1
            from  sysobjects
           where  id = object_id('TBlogTagMapper')
            and   type = 'U')
   drop table TBlogTagMapper
go
create table TBlogTagMapper (
   BlogId               uniqueidentifier     not null,
   TagName              nvarchar(20)         not null,
   constraint PK_TBLOGTAGMAPPER primary key (BlogId, TagName)
)
go
/*==============================================================*/
/* Table: TTag                                                  */
/*==============================================================*/
if exists (select 1
            from  sysobjects
           where  id = object_id('TTag')
            and   type = 'U')
   drop table TTag
go
create table TTag (
   UserName             nvarchar(20)         not null,
   TagName              nvarchar(20)         not null,
   constraint PK_TTAG primary key (UserName, TagName)
)
go