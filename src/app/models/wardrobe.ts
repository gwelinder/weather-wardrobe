import { WeatherCondition } from './weather';

export interface ClothingItem {
  clothingItemId: number;
  name: string;
  description: string;
  imageURL: string;
  weatherConditions: WeatherCondition[];
}
