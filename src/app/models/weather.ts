import { ClothingItem } from './wardrobe';

export interface WeatherCondition {
  weatherConditionId: number;
  conditionName: string;
  temperatureRange: string;
  temperature?: number;
}

export interface WeatherRecommendation {
  weatherCondition?: WeatherCondition;
  temperature?: number;
  location: string;
  recommendedItems: ClothingItem[];
}

export class WeatherConditionImpl implements WeatherCondition {
  weatherConditionId: number;
  conditionName: string;
  temperatureRange: string;
  temperature?: number;

  constructor(
    weatherConditionId: number,
    conditionName: string,
    temperatureRange: string,
    temperature?: number
  ) {
    this.weatherConditionId = weatherConditionId;
    this.conditionName = conditionName;
    this.temperatureRange = temperatureRange;
    this.temperature = temperature;
  }
}
