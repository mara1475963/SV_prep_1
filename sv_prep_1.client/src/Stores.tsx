import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface Store {
    id: number,
    name: string,
    formatedAddress: string
}

const Stores: React.FC = () => {
    const navigate = useNavigate();

    const [stores, setStores] = useState<Store[] | []>([]);
    const [isLoading, setIsLoading] = useState<boolean>(false);

    const redirectToStoreProducts = (storeId: number) => {
        navigate(`/stores/${storeId}/products`);
    }

    const getStores = async () => {
        try {
            setIsLoading(true);
            const response = await fetch('https://localhost:7233/api/Stores');
            if (response.ok) {
                const data: Store[] = await response.json();
                setStores(data)
                console.log(data)
                setIsLoading(false);
             
            }
        }
        catch (error) {
            setIsLoading(false)
            console.error('Error during fetching products:', error);
        }
    }


    useEffect(() => {
        getStores();
    }, []);

    return (
         isLoading ? <h3>Loading...</h3 > :
         <div>
        {stores?.map((store, index) => (
            <div key={index}>
                <span><b>{store.name}</b> {`${store.formatedAddress},-`}</span>

                <button type="button" onClick={() => redirectToStoreProducts(store.id)}>Open</button>
            </div>
        ))}
    </div>

    );


}

export default Stores;
