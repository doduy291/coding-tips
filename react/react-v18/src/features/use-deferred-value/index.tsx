import React, { useState } from "react";
import { generateProducts } from "../../data";
import ProductList from "./ProductList";

const dummyProducts = generateProducts();

const filterProducts = (filterTerm: string) => {
  if (!filterTerm) {
    return dummyProducts;
  }
  return dummyProducts.filter((product) => product.includes(filterTerm));
};
const UseDeferredValueExample = () => {
  const [filterTerm, setFilterTerm] = useState("");

  const filteredProducts = filterProducts(filterTerm);

  const updateFilterHandler = (e: React.ChangeEvent<HTMLInputElement>) => {
    setFilterTerm(e.target.value);
  };

  return (
    <div>
      <input type="text" onChange={updateFilterHandler} />
      <ProductList products={filteredProducts} />
    </div>
  );
};

export default UseDeferredValueExample;
