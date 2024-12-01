import { useEffect, useState } from 'react';
import './App.css';
import { useForm, SubmitHandler } from 'react-hook-form';

interface Product {
    id: number,
    name: string,
    price: number
}


const  App: React.FC = () => {
    const [products, setProducts] = useState<Product[] | []>([]);
    const [selectedProduct, setSelectedProduct] = useState<Product | null>(null);

    const getProducts = async () => {
        const response = await fetch('https://localhost:7233/api/Products');
        if (response.ok) {
            const data: Product[] = await response.json();
            setProducts(data)
        }
    }
    useEffect(() => {
        getProducts();
    }, []);

    const { register, handleSubmit, formState: { errors }, setValue, reset } = useForm<Product>();
    const onSubmit: SubmitHandler<Product> = async (data) => {
        try {
            let url = ""
            if (selectedProduct) {
                url = `https://localhost:7233/api/Products/${selectedProduct.id}`;
            } else {
                url = 'https://localhost:7233/api/Products';
            }
            const response = await fetch(url, {
                method: selectedProduct ? 'PUT' : 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    name: data.name,
                    price: data.price,
                }),
            });

            if (response.ok) {
                //const result = await response.json();
                console.log('Success:', response);
                reset();
                getProducts();
                setSelectedProduct(null);
            } else {
                console.error('Failed to create product:', await response.text());
            }
        } catch (error) {
            console.error('Error during product creation:', error);
        }
    }

    const handleOnEditClick = (product: Product) => {
        setValue('name', product.name)
        setValue('price', product.price)
        setSelectedProduct(product);
    }

    const handleOnAddNewClick = () => {
        setSelectedProduct(null);
        reset();
    }

    const handleOnDeleteClick = async (productId: number) => {
        try {
            const response = await fetch(`https://localhost:7233/api/Products/${productId}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                },
                
            });

            if (response.ok) {
                console.log('Product deleted:', response);
                reset();
                getProducts();
                setSelectedProduct(null);

            } else {
                console.error('Failed to delete product:', await response.text());
            }
        } catch (error) {
            console.error('Error during product creation:', error);
        }
    }
   
  
    return (
        <div>
            {products?.map((product, index) => (
                <span key={index}>
                    <p>{`Name: ${product.name}, Price: ${product.price}`}</p>
                    <button type="button" onClick={() => handleOnEditClick(product)}>Upravit</button>
                    <button type="button" onClick={() => handleOnDeleteClick(product.id)}>Odstranit</button>
              </span>
            ))}
            

            < div style={{ border: "solid 1px black" }}>
                {selectedProduct ? (<h2>Update product</h2>) : (<h2>Add product</h2>)}
                <form onSubmit={handleSubmit(onSubmit)} >

                    <label>Name: </label>
                    <input type="text" {...register("name")}  /><br />
                    {errors.name && <div>This field is required</div>}


                    <label>Price: </label>
                    <input type={"number"} {...register("price", { required: true })}  /><br />

                    {errors.price && <div>This field is required</div>}

                    {selectedProduct ? (<button type="submit" >Update</button>) : (<button type="submit" >Add</button>)}
                </form>
            </div>

            <button type="button" onClick={handleOnAddNewClick}>Add new</button>
        </div>
    );


}

export default App;
