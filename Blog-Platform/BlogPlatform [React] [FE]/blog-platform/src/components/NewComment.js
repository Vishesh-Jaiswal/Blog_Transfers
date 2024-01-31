import { useState } from "react";
import './NewComment.css';

function NewComment({ blogId, onCommentAdded,onCancel}){
    const [content,setContent]=useState("");
    const userEmail = localStorage.getItem('userEmail');
    var [noContentError,setnoContentError]=useState("");
    var comment;

    var checkBlogInfo = ()=>{
        if(content==='')
        {
            setnoContentError("Empty Content Body");
            return false;
        }
        return true;
    }

    const handleSubmit =async (event)=>{
        event.preventDefault();
        var checkData = checkBlogInfo();
        if(checkData===false)
        {
            alert('please check your data')
            return;
        }
        comment={
            blogId: blogId,
            content: content,
            userEmail: userEmail
        }
        try {
            const response = await fetch("http://localhost:5273/api/Comment/AddComment", {
              method: 'POST',
              headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + localStorage.getItem("token")
              },
              body: JSON.stringify(comment),
            });
            if (response.ok) {
              setContent("");
              onCommentAdded();
            } else {
              console.error("Error adding comment:", response.statusText);
            }
          } catch (err) {
            console.error("Error adding comment:", err);
          }
        }

    return(
        <form className="addComment" onSubmit={handleSubmit}>
            <input placeholder="Type your comment here" type="text" className="form-control" value={content}
                    onChange={(e)=>{setContent(e.target.value)}}/>
            {noContentError && <label className="alert alert-danger">{noContentError}</label>}   
            <button type="submit" className="btn btn-primary button">Post Comment</button>
            {onCancel && (
              <button type="button" className="btn btn-danger button" onClick={onCancel}>
                Cancel
              </button>
            )}
        </form>
    );
}

export default NewComment;