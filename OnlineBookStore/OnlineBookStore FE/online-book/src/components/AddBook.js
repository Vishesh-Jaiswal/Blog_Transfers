import axios from "axios";
import { useDebugValue, useState } from "react";
import Navbar from "./Navbar";
import './AddBook.css';

function AddBook(){
    const [title,setTitle]=useState("");
    const [author,setAuthor]=useState("");
    const [genre, setGenre]=useState("");
    const [isbn,setIsbn]=useState("");
    var userEmail=localStorage.getItem('userEmail');
    const handleAddBook=()=>{
        axios.post("http://localhost:5204/api/Book/AddBook",{
            title:title,
            author:author,
            genre:genre,
            isbn:isbn
        })
        .then((response)=>{

        }).catch((error)=>{

        })
    }
    return(
        <div>
            <Navbar/>
            <div className="formtype">
  
            <form>
                <input value={title} onChange={(e)=>setTitle(e.target.value)}/>
                <input value={author} onChange={(e)=>setAuthor(e.target.value)}/>
                <input value={genre} onChange={(e)=>setGenre(e.target.value)}/>
                <input value={isbn} onChange={(e)=>setIsbn(e.target.value)}/>
                <input type="button" value="Add Book" onClick={handleAddBook} />
            </form>
                          
            </div>
        </div>
    )
}
export default AddBook;