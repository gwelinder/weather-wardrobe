import { ClothingItem } from './wardrobe';

export interface WeatherCondition {
  WeatherConditionId: number;
  ConditionName: string;
  TemperatureRange: string;
  Temperature: number;
}

export interface WeatherRecommendation {
  WeatherCondition: WeatherCondition;
  Temperature: number;
  Location: string;
  RecommendedItems: ClothingItem[];
}

export class WeatherConditionImpl implements WeatherCondition {
  WeatherConditionId: number;
  ConditionName: string;
  TemperatureRange: string;
  Temperature: number;

  constructor(
    weatherConditionId: number,
    conditionName: string,
    temperatureRange: string,
    temperature: number
  ) {
    this.WeatherConditionId = weatherConditionId;
    this.ConditionName = conditionName;
    this.TemperatureRange = temperatureRange;
    this.Temperature = temperature;
  }
}
