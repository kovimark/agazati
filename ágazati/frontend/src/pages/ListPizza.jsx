import { useEffect, useState } from "react"
import Card from "../components/Card"
import axios from "axios"
import React from "react";

function ListPizza() {

    const [pizzas, setPizzas] = useState([])
    const URL = "https://pizza.sulla.hu/pizza/"

    useEffect(() => {
      GetData()
    }, [])

    function GetData() {
        axios.get(URL)
        .then(function(response) {
            console.log(response);
            setPizzas(response.data)
        })
        
    }

    
    


    return (
        <div className="row d-flex justify-content-center">
            {
                pizzas.map(function(pizza) {
                    
                    return <Card key={pizza.id} pizza={pizza} GetData={GetData}/>
                })
            }
        </div>
    )
}


export default ListPizza;