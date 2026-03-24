#pragma once

#include <string>
#include <vector>
#include "Category.h"
#include "nlohmann/json.hpp"

using json = nlohmann::json;

/**
 * Класс Tested представляет тестируемого пользователя.
 * Логика живёт внутри DLL, WinForms будет работать через обёртки.
 */
class Tested {
private:
    std::string name_;                 // Имя пользователя
    std::vector<Category*> available_; // Доступные категории
    int total_points_ = 0;             // Сумма баллов по всем пройденным категориям
    int passed_categories_ = 0;        // Сколько категорий реально пройдено

public:
    // Конструкторы
    Tested();
    explicit Tested(const std::string& name);

    // Геттеры/сеттеры
    const std::string& getName() const;
    void setName(const std::string& name);

    int getTotalPoints() const;
    int getPassedCategories() const;

    // Работа со списком категорий
    void setAvailableCategories(const std::vector<Category*>& cats);
    const std::vector<Category*>& getAvailableCategories() const;
    void addAvailableCategory(Category* cat);

    // Логика тестирования
    int startCategory(Category& category);
    double getAverage() const;
    void printResults() const;

    // Сохранение/загрузка результатов
    json toJson() const;
    void fromJson(const json& j);
    bool saveResultsToFile(const std::string& filename) const;
    bool loadResultsFromFile(const std::string& filename);
};
