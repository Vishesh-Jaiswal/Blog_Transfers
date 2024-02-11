--HELp------------------
sp_help Users
sp_help UserFollowers
sp_help Blogs
sp_help Comments
sp_help Categories
sp_help CommentLikes
sp_help BlogLikes

-----SELECT---------------------------------------------------
select * from Blogs
select * from users
select * from Comments
select * from Categories
select * from BlogLikes
select * from CommentLikes
select * from UserFollowers
--Delete------------------------------------------------
delete from Blogs
delete from Categories
delete from Comments
delete from BlogLikes
delete from CommentLikes

----DATABASE-------------------------------------------------

use master
drop database BlogSpotDB
create database BlogSpotDB
use BlogSpotDB