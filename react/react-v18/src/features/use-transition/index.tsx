import React, { useState, useTransition } from "react";
import { generateProducts } from "../../data";
import ProductList from "./ProductList";

const dummyProducts = generateProducts();

const filterProducts = (filterTerm: string) => {
  if (!filterTerm) {
    return dummyProducts;
  }
  return dummyProducts.filter((product) => product.includes(filterTerm));
};
const UseTransitionExample = () => {
  const [isPending, startTransition] = useTransition();
  const [filterTerm, setFilterTerm] = useState("");

  const filteredProducts = filterProducts(filterTerm);

  const updateFilterHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
    startTransition(() => {
      setFilterTerm(e.target.value);
    });
  };

  return (
    <div>
      <input type="text" onChange={updateFilterHandler} />
      {isPending && <div>Pending...</div>}
      <ProductList products={filteredProducts} />
    </div>
  );
};

export default UseTransitionExample;
