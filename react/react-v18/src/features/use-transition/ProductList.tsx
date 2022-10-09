interface Props {
  products: Array<string>;
}
const ProductList: React.FC<Props> = ({ products }) => {
  return (
    <ul>
      {products.map((product) => (
        <li key={product}>{product}</li>
      ))}
    </ul>
  );
};

export default ProductList;
