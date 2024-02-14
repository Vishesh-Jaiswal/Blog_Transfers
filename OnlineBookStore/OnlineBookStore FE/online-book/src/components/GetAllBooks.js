import axios from "axios";
import { useEffect, useState } from "react";
import { useSearchParams } from "react-router-dom";

function GetAllBooks(){
    const [books,setBooks]=useState([]);


        const fetchBooks=async()=>{
            const handleAGetallBooks=axios.get("http://localhost:5204/api/Book/GetAllBooks")
            .then((event1)=>{
                setBooks(event1);
            })
            setBooks(handleAGetallBooks|| []);
        }
   
        
    
    return(
        <div>
            <ul>
                {books.map((book)=>(
                    <li key={book.bookId}>
                        {book.title}
                    </li>
                ))}
                
            </ul>
<button onClick={fetchBooks}>Get</button>
        </div>
    )
}
export default GetAllBooks;