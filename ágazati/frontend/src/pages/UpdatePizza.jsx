import axios from 'axios';
import { useEffect, useState } from 'react'
import { useNavigate, useParams } from 'react-router-dom';

export default function UpdatePizza() {

    const [pizzaName, setPizzaName] = useState("");
    const [pizzaImage, setPizzaImage] = useState("")
    const navigate = useNavigate();
    const {id} = useParams();
    const URL = "https://pizza.sulla.hu/pizza/"

    useEffect(() => {
            GetDataId()
    
    
        }, [])

    function GetDataId() {
        axios.get(URL + id)
            .then(function (response) {
                setPizzaName(response.data.name);
                setPizzaImage(response.data.image_url)
            })
    }

    function PizzaNameChange(event) {
        setPizzaName(event.target.value)
    }

    function SubmitPizza(event) {
        event.preventDefault();

        let data = {
            name: pizzaName,
            image_url: document.getElementById("image_url").value
        }

        axios.put(URL + id, data)
        .then(function() {
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
                    <input type="text" defaultValue={pizzaName} class="form-control" id="name" aria-describedby="nameHelp" onChange={PizzaNameChange} />
                    <div id="nameHelp" class="form-text" style={{
                        color: !pizzaName ? "red" : "green"
                    }}>Fantáziadús pizza név</div>
                </div>
                <div class="mb-3">
                    <label htmlFor="image_url" class="form-label">Pizza kép URL-je</label>
                    <input type="text" defaultValue={pizzaImage} class="form-control" id="image_url" />
                </div>
                <button type="submit" class="btn btn-primary">Küldés</button>
            </form>
        </div>
    )
}
