import { useDeferredValue } from "react";

interface Props {
  products: Array<string>;
}
const ProductList: React.FC<Props> = ({ products }) => {
  const deferredProducts = useDeferredValue(products);
  return (
    <ul>
      {deferredProducts.map((product) => (
        <li key={product}>{product}</li>
      ))}
    </ul>
  );
};

export default ProductList;
