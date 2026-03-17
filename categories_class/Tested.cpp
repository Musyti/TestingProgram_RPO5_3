// Tested.cpp
#include "Tested.h"
#include <fstream>
#include <iostream>

Tested::Tested() : name_("guest") {}
Tested::Tested(const std::string& name) : name_(name) {}

const std::string& Tested::getName() const { return name_; }
void Tested::setName(const std::string& name) { name_ = name; }

int Tested::getTotalPoints() const { return total_points_; }
int Tested::getPassedCategories() const { return passed_categories_; }

void Tested::setAvailableCategories(const std::vector<Category*>& cats) {
    available_ = cats;
}

const std::vector<Category*>& Tested::getAvailableCategories() const {
    return available_;
}

void Tested::addAvailableCategory(Category* cat) {
    if (cat) available_.push_back(cat);
}

int Tested::startCategory(Category& category) {
    if (!category.isActive() && !category.isFinished()) {
        category.start();
    }
    if (!category.isActive()) {
        std::cout << "Категория недоступна для запуска." << std::endl;
        return 0;
    }
    int earned = category.end();
    total_points_ += earned;
    ++passed_categories_;
    return earned;
}

double Tested::getAverage() const {
    if (passed_categories_ == 0) return 0.0;
    return static_cast<double>(total_points_) / passed_categories_;
}

void Tested::printResults() const {
    std::cout << "=== Результаты пользователя " << name_ << " ===\n";
    std::cout << "Всего баллов: " << total_points_ << "\n";
    std::cout << "Пройдено категорий: " << passed_categories_ << "\n";
    std::cout << "Средний результат: " << getAverage() << "\n";
}

json Tested::toJson() const {
    json j;
    j["name"] = name_;
    j["total_points"] = total_points_;
    j["passed_categories"] = passed_categories_;
    j["average"] = getAverage();
    return j;
}

void Tested::fromJson(const json& j) {
    name_ = j.value("name", "guest");
    total_points_ = j.value("total_points", 0);
    passed_categories_ = j.value("passed_categories", 0);
}

bool Tested::saveResultsToFile(const std::string& filename) const {
    try {
        json j = toJson();
        std::ofstream file(filename);
        if (!file.is_open()) {
            throw std::runtime_error("Не удалось открыть файл: " + filename);
        }
        file << j.dump(4);
        return true;
    } catch (const std::exception& e) {
        std::cerr << "Ошибка при сохранении результатов: " << e.what() << std::endl;
        return false;
    }
}

bool Tested::loadResultsFromFile(const std::string& filename) {
    try {
        std::ifstream file(filename);
        if (!file.is_open()) {
            throw std::runtime_error("Не удалось открыть файл: " + filename);
        }
        json j;
        file >> j;
        fromJson(j);
        return true;
    } catch (const std::exception& e) {
        std::cerr << "Ошибка при загрузке результатов: " << e.what() << std::endl;
        return false;
    }
}
