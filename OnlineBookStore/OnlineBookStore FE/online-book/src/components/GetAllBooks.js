import axios from "axios";
import { useEffect, useState } from "react";
import { Link, useSearchParams } from "react-router-dom";

function GetAllBooks(){
    const [books,setBooks]=useState([]);


        const fetchBooks=async()=>{
            const handleAGetallBooks=await axios.get("http://localhost:5204/api/Book/GetAllBooks")
            setBooks(handleAGetallBooks.data);
       
        }
   
        
    
    return(
        <div>
            <ul>
                {books.map((book)=>(
                    <li key={book.bookId}>
                        <Link to={`/getbookbyid/${book.bookId}`}>
                        {book.title}</Link>
                    </li>
                ))}
                
            </ul>
<button onClick={fetchBooks}>Get</button>
        </div>
    )
}
export default GetAllBooks;