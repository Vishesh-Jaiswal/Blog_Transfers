import React, { useState, useEffect } from "react";
import axios from "axios";
import "./GetBlogs.css";
import { Link } from "react-router-dom";
import Navbar from "./Navbar";

function GetBlogs() {
  const [blogs, setBlogs] = useState([]);
  const [categories, setCategories] = useState({});

  useEffect(() => {
    const fetchBlogs = async () => {
      try {
        const blogResponse = await axios.get('http://localhost:5273/api/Blog');
        setBlogs(blogResponse.data|| []);

        // Fetch categories for each blog
        const categoryPromises = blogResponse.data.map(async (blog) => {
          const categoryResponse = await axios.get(`http://localhost:5273/api/Category/${blog.blogId}`);
          return { blogId: blog.blogId, categories: categoryResponse.data };
        });

        const blogCategories = await Promise.all(categoryPromises);

        const categoriesMap = {};
        blogCategories.forEach(({ blogId, categories }) => {
          categoriesMap[blogId] = categories;
        });

        setCategories(categoriesMap);

      } catch (error) {
        console.error('Error fetching blogs:', error);
      }
    };

    fetchBlogs();
  }, []);

  return (
    <div className="show-blogs">
      <Navbar />
      <div className="blog-list-container">
        <div className="blogContents">
          <h2 className="PageTitle">Blogs</h2>
          {Array.isArray(blogs) && blogs.length > 0 ? (
            <ul>
              {blogs.map((blog) => (
                <li key={blog.blogId} className="blog-item">
                  <Link to={`/showblogs/${blog.blogId}`} className="blog-link">
                    <h3 className="blog-title">{blog.title}</h3>
                  </Link>
                  <p className="blog-content">{blog.content}</p>
                  <p className="author-info">Author: {blog.userEmail}</p>

                  {categories[blog.blogId] && (
                    <div className="categories">
                      <p>Categories:</p>
                      
                      <ul className="categoryItems">
                        {categories[blog.blogId].map((category) => (
                          <li className="categoryItem" key={category.relationId}>{category.categoryName}</li>
                        ))}
                      </ul>
                      
                    </div>
                  )}
                </li>
              ))}
            </ul>
          ) : (
            <p id="EmplyBlogList">No Blog Available</p>
          )}
        </div>
      </div>
    </div>
  );
}

export default GetBlogs;
