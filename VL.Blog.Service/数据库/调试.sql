Use [VL.Blog];
select * from TBlog;
select * from TBlogDetail;
select * from TTag;
select * from TBlogTagMapper;

-- �������� ĳ�û���ĳƪ���͵ı�ǩ 
select * from TTag where UserName=@UserName and TagId in (select TagId from TBlogTagMapper where BlogId=@BlogId)
-- ��Ϊ��Ч������
select t.* from TBlogTagMapper b,TTag t where b.BlogId=@BlogId and b.TagId=t.TagId

-- ��ձ�ǩ