type Dog = {
    name: string;
    birthYear: number;
    imageUrl: string;
}


const URL_BASE = "https://dogcoders.azurewebsites.net/Dogs";

export const getAllDogs = async (): Promise<Dog[]> => {
    const result = await fetch(URL_BASE).then(result => result.json())
    return result as Dog[];
}