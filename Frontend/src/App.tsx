
import { useQuery } from 'react-query'
import './App.css'
import { getAllDogs } from './api/DogAPI'

function App() {

  const {data, isError, isLoading} = useQuery({
    queryKey: ['month'], 
    queryFn: getAllDogs})

  return (
    <>
    {isError && <p>An error occured...</p>}
    {isLoading && <p>Loading...</p>}
    {data && data.map(dog =>
      <div>
        <h1>{dog.name}</h1>
        {dog.imageUrl && <img width="500px" height="500px" src={dog.imageUrl} alt="Picture of dog" />}
        <h2>{dog.birthYear}</h2>
      </div>
    )}
    </>
  )
}

export default App
