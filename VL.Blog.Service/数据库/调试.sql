Use [VL.Blog];
select * from TBlog;
select * from TBlogDetail;
select * from TTag;
select * from TBlogTagMapper;

-- 关联查找 某用户的某篇博客的标签 
select * from TTag where UserName=@UserName and TagId in (select TagId from TBlogTagMapper where BlogId=@BlogId)
-- 更为高效的做法
select t.* from TBlogTagMapper b,TTag t where b.BlogId=@BlogId and b.TagId=t.TagId

-- 清空标签