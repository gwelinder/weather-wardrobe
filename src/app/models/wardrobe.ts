import { WeatherCondition } from './weather';

export interface ClothingItem {
  ClothingItemId?: number;
  Name: string;
  Description: string;
  ImageURL: string;
  WeatherConditions?: WeatherCondition[];
  WeatherConditionIds?: number[];
}
