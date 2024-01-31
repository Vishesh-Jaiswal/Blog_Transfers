import React from 'react';

const BlogButton = ({ userRole, onClick }) => {
  return userRole === 'blogger' ? (
    <button onClick={onClick}>Add Blog</button>
  ) : (
    <button disabled>Read Blogs</button>
  );
};

export default BlogButton;