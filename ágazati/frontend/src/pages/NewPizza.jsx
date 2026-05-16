import axios from 'axios';
import { useState } from 'react'
import { useNavigate } from 'react-router-dom';

export default function NewPizza() {
    const [name, setName] = useState("");
    const navigate = useNavigate();
    const URL = "https://pizza.sulla.hu/pizza/"

    function PizzaNameChange(event) {
        setName(event.target.value)
    }

    function SubmitPizza(event) {
        event.preventDefault();

        let data = {
            name: name,
            image_url: document.getElementById("image_url").value
        }

        axios.post(URL, data)
        .then(function(res) {
            alert("Sikeres feltöltés!");
            navigate("/pizzak")
        })
        .catch(function(error) {
            console.log(error);
            
        })
    }

    return (
        <div className='d-flex justify-content-center'>
            <form className='m-3 w-25' onSubmit={SubmitPizza}>
                <div class="mb-3">
                    <label htmlFor="name" class="form-label">Pizza neve</label>
                    <input type="text" class="form-control" id="name" aria-describedby="nameHelp" onChange={PizzaNameChange} />
                    <div id="nameHelp" class="form-text" style={{
                        color: !name ? "red" : "green"
                    }}>Fantáziadús pizza név</div>
                </div>
                <div class="mb-3">
                    <label htmlFor="image_url" class="form-label">Pizza kép URL-je</label>
                    <input type="text" class="form-control" id="image_url" />
                </div>
                <button type="submit" class="btn btn-primary">Küldés</button>
            </form>
        </div>
    )
}
