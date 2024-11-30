import { useEffect, useState } from 'react';
import './App.css';

interface Product {
    id: number,
    name: string,
    price: number
}

const  App: React.FC = () => {
    const [products, setProducts] = useState<Product[] | []>([]);

    const getProducts = async () => {
        const response = await fetch('https://localhost:7233/api/Products');
        if (response.ok) {
            const data: Product[] = await response.json();
            setProducts(data)
            console.log(data)
        }
    }
    useEffect(() => {
        getProducts();
    }, []);

  
    return (
        <div>
            {products?.map((product, index) => (
              
                <p key={index }>{`Product ID: ${product.id}, Name: ${product.name}, Price: ${product.price}`}</p>
              
            ))}
        </div>
    );


}

export default App;
