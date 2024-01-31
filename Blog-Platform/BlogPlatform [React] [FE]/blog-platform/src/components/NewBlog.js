import { useState } from "react";
import './NewBlog.css';
import Navbar from "./Navbar";

function NewBlog(){
    
    const [title, setTitle] = useState("");
    const [content, setContent] = useState("");
    const [selectedCategories, setSelectedCategories] = useState([]);
    const [availableCategories, setAvailableCategories] = useState([
        "Technology",
        "Lifestyle",
        "PD Essentials",
        "Business","Finance",
        "Science","Education",
        "Entertainment",
        "News","Current Affairs",
        "Parenting",
        "Environment","Sustainability",
        "Sports",
   
      ]);
    const userEmail = localStorage.getItem('userEmail');
    const [noTitleError, setNoTitleError] = useState("");
    const [noContentError, setNoContentError] = useState("");
    const [noCotegories, setNoCotegoriesError] = useState("");

    const checkBlogInfo = () => {
        if (title === '') {
            setNoTitleError("No Title Provided");
            setTimeout(() => setNoTitleError(""), 4000);
            return false;
        }
        if (content === '') {
            setNoContentError("Empty Content Body");
            setTimeout(() => setNoTitleError(""), 4000);
            return false;
        }
        if(selectedCategories.length === 0){
            setNoCotegoriesError("No Categories Selected");
            setTimeout(() => setNoTitleError(""), 4000);
            return false;
        }
        return true;
    }

    const handleCategoryClick = (category) => {
        const updatedSelectedCategories = [...selectedCategories, category];
        const updatedAvailableCategories = availableCategories.filter(cat => cat !== category);
        setSelectedCategories(updatedSelectedCategories);
        setAvailableCategories(updatedAvailableCategories);
    }

    const handleRemoveCategory = (category) => {
        const updatedSelectedCategories = selectedCategories.filter(cat => cat !== category);
        const updatedAvailableCategories = [...availableCategories, category];
        setSelectedCategories(updatedSelectedCategories);
        setAvailableCategories(updatedAvailableCategories);
    }

    const AddPost = async (event) => {
        event.preventDefault();
        const checkData = checkBlogInfo();
        if (!checkData) {
            alert('Please check your data');
            return;
        }
    
        const blog = {
            title: title,
            content: content,
            userEmail: userEmail,
            categories: selectedCategories,
        };
    
        try {
            const response = await fetch("http://localhost:5273/api/Blog/Create", {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + localStorage.getItem("token")
                },
                body: JSON.stringify(blog)
            });
    
            if (response.ok) {
                setTitle("");
                setContent("");
                setSelectedCategories([]);
                setNoTitleError("");
                setNoContentError("");
                alert('Blog posted successfully');
            } else {
                alert('Failed to post blog. Please try again.');
            }
        } catch (error) {
            console.log(error);

        }
    };
    

    return (
        <div className="MainBody">
            <Navbar />
            <div className="newBlogContainer">
                <form className="blogFormCreate" >
                    <div className="categoryContainer1">
                        <h4 id="Category">Categories</h4>
                        <div className="categoryContainer">
                            <br/>
                            {availableCategories.map(category => (
                                <button key={category} onClick={() => handleCategoryClick(category)}>{category}</button>
                            ))}
                        </div>
                    </div>
                    <div className="NewBlogArea">
                        <input type="text" id="title" placeholder="Title" className="form-control" value={title} 
                            onChange={(e) => setTitle(e.target.value)} />
                            <br/>
                        {noTitleError && <label  className="alert alert-danger">{noTitleError}</label>}
                        <div className="categoryHolder">
                            <div className="selectedCategories">
                                {selectedCategories.map(category => (
                                    <button key={category} onClick={() => handleRemoveCategory(category)}>{category}</button>
                                ))}
                            </div>
                            {noCotegories && <label className="alert alert-danger">{noCotegories}</label>}
                        </div> <br/>
                        <div className="contentHolder">
                            <textarea placeholder="Type Your Content Here...." className="form-control" rows="5" cols="33" value={content}
                                onChange={(e) => setContent(e.target.value)}></textarea>
                        </div>
                        <br/>
                        <div className="blogButtons">
                            {noContentError && <label className="alert alert-danger">{noContentError}</label>}
                            <button className="btn btn-danger button" >Cancel</button>
                            <button className="btn btn-primary button" onClick={AddPost}>Submit Post</button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default NewBlog;