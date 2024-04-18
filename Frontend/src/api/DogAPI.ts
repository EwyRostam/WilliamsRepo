type Dog = {
    name: string;
    birthYear: number;
    imageUrl: string;
}


const URL_BASE = "http://localhost:5092/Dogs";
const headers = {'Content-type': "application/json; charset=UTF-8"}

export const getAllDogs = async (): Promise<Dog[]> => {
    const result = await fetch(URL_BASE).then(result => result.json())
    return result as Dog[];
}