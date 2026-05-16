import React from "react";
import { render, screen } from "@testing-library/react";
import axios from "axios";
import ListPizza from "./ListPizza";
import { vi } from "vitest";


vi.mock("axios");

vi.mock("../components/Card", () => ({
  default: function MockCard({ pizza }) {
    return <div>{pizza.name}</div>;
  },
}));

test("megjelenít egy adott pizzát a GET kérés után", async () => {
  axios.get.mockResolvedValueOnce({
    data: [
                {
                    id: 1,
                    name: "Pizza Margherita",
                    image_url: "https://upload.wikimedia.org/wikipedia/commons/thumb/c/c8/Pizza_Margherita_stu_spivack.jpg/1024px-Pizza_Margherita_stu_spivack.jpg"
                },
            ],
        });

  render(<ListPizza />);

  expect(await screen.findByText("Pizza Margherita")).toBeInTheDocument();
});