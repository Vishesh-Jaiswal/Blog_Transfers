import axios from "axios";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";

function GetOneBook(){
    const {bookId}=useParams();
    const [book,setBook]=useState(null);
    useEffect(()=>{
        const getBook= async ()=>{
            const response=await axios.get(`http://localhost:5204/api/Book/GetBookById/${bookId}`)
            setBook(response.data);
        }
        getBook();
    },[bookId])
    
    return(
        <div>
<p>{book?.title}</p>
        </div>
    )
}
export default GetOneBook;