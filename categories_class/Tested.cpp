
#include "Tested.h"
#include <fstream>
#include <iostream>

// --- Конструкторы ---

Tested::Tested()
    : name_("guest")
    , available_()
    , total_points_(0)
    , passed_categories_(0) {
}

Tested::Tested(const std::string& name)
    : name_(name)
    , available_()
    , total_points_(0)
    , passed_categories_(0) {
}

// --- Геттеры / сеттеры ---

const std::string& Tested::getName() const {
    return name_;
}

void Tested::setName(const std::string& name) {
    name_ = name;
}

int Tested::getTotalPoints() const {
    return total_points_;
}

int Tested::getPassedCategories() const {
    return passed_categories_;
}

// --- Категории ---

void Tested::setAvailableCategories(const std::vector<Category*>& cats) {
    available_ = cats;
}

const std::vector<Category*>& Tested::getAvailableCategories() const {
    return available_;
}

void Tested::addAvailableCategory(Category* cat) {
    if (cat) {
        available_.push_back(cat);
    }
}

// --- Логика тестирования ---

int Tested::startCategory(Category& category) {
    // если ещё не активна и не завершена — запускаем
    if (!category.isActive() && !category.isFinished()) {
        category.start();
    }

    if (!category.isActive()) {
        std::cout << "Категория недоступна для запуска." << std::endl;
        return 0;
    }

    // Здесь предполагается, что WinForms уже проставил
    // is_resolved_/is_correct_ в вопросах.
    int earned = category.end();

    total_points_ += earned;
    ++passed_categories_;
    return earned;
}

double Tested::getAverage() const {
    if (passed_categories_ == 0) {
        return 0.0;
    }
    return static_cast<double>(total_points_) / passed_categories_;
}

void Tested::printResults() const {
    std::cout << "=== Результаты пользователя " << name_ << " ===\n";
    std::cout << "Всего баллов: " << total_points_ << "\n";
    std::cout << "Пройдено категорий: " << passed_categories_ << "\n";
    std::cout << "Средний результат: " << getAverage() << "\n";
}

// --- JSON ---

json Tested::toJson() const {
    json j;
    j["name"] = name_;
    j["total_points"] = total_points_;
    j["passed_categories"] = passed_categories_;
    j["average"] = getAverage();
    return j;
}

void Tested::fromJson(const json& j) {
    name_ = j.value("name", std::string("guest"));
    total_points_ = j.value("total_points", 0);
    passed_categories_ = j.value("passed_categories", 0);
}

// --- Работа с файлами ---

bool Tested::saveResultsToFile(const std::string& filename) const {
    try {
        json j = toJson();
        std::ofstream file(filename);
        if (!file.is_open()) {
            throw std::runtime_error("Не удалось открыть файл: " + filename);
        }

        file << j.dump(4);
        file.close();
        std::cout << "Результаты пользователя сохранены в " << filename << std::endl;
        return true;
    }
    catch (const std::exception& e) {
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
        file.close();

        fromJson(j);
        std::cout << "Результаты пользователя загружены из " << filename << std::endl;
        return true;
    }
    catch (const std::exception& e) {
        std::cerr << "Ошибка при загрузке результатов: " << e.what() << std::endl;
        return false;
    }
}

