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
delete from TTag;
delete from TBlogTagMapper;


select BlogId,Content from TBlogDetail where BlogId ='d8aecbb1-33f4-4b39-839d-3618f383a234'