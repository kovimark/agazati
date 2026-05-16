import { useEffect, useState } from "react"
import Card from "../components/Card"
import { useParams } from "react-router-dom"
import axios from "axios"

export default function SinglePizza() {

    const [pizza, setPizza] = useState({})
    const URL = "https://pizza.sulla.hu/pizza/"

    useEffect(() => {
        GetData()


    }, [])

    const params = useParams();
    const id = params.id;

    function GetData() {
        axios.get(URL + id)
            .then(function (response) {
                setPizza(response.data)
            })
    }

    if(!pizza) {
        return (
            <div>Betöltés...</div>
        )
    }


    return (
        <div className="row d-flex justify-content-center">
            <div className="card" style={{ width: "18rem" }}>
                <img src={pizza.image_url} className="card-img-top " alt="..." />
                <div className="card-body">
                    <h5 className="card-title">{pizza.name}</h5>
                </div>
            </div>
        </div>
    )
}
